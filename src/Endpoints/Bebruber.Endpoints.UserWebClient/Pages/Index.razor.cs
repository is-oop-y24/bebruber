using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bebruber.Application.Requests.Rides.Commands;
using Bebruber.Common.Dto;
using Bebruber.Endpoints.Shared.Interfaces;
using Bebruber.Endpoints.Shared.Models;
using Bebruber.Endpoints.SignalR.Users;
using Bebruber.Endpoints.UserWebClient.Clients;
using Blazored.LocalStorage;
using FisSst.BlazorMaps;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using SignalR.Strong;
using Map = Bebruber.Endpoints.Shared.Components.Map;
using Marker = Bebruber.Endpoints.Shared.Models.Marker;
using MarkerConfig = Bebruber.Endpoints.Shared.Models.MakerConfig;
using Polyline = Bebruber.Endpoints.Shared.Models.Polyline;

namespace Bebruber.Endpoints.UserWebClient.Pages;

public enum SelectionState
{
    None = 0,
    StartPoint,
    EndPoint,
    ExtraPoint
}

public enum RideState
{
    Selecting = 0,
    WaitingDriver = 1,
    Riding = 2
}

public partial class Index
{
    private static readonly string _bebraGreenPath = "/_content/Bebruber.Endpoints.Shared/img/bebra-green.png";
    private static readonly string _bebraRedPath = "/_content/Bebruber.Endpoints.Shared/img/bebra-red.png";
    private static readonly string _bebraBluePath = "/_content/Bebruber.Endpoints.Shared/img/bebra-blue.png";
    private static readonly string _bebraTaxi = "/_content/Bebruber.Endpoints.Shared/img/car.png";
    private static readonly Point _bebraSize = new Point(54 / 2, 95 / 2);
    private static readonly Point _bebraAnchor = new Point(54 / 4, 95 / 2);
    private List<string> _taxiCategories;
    private readonly MarkerConfig _markerConfig = new MarkerConfig();
    private readonly RideState _currentRideState = RideState.Selecting;
    private Marker _startPointMarker;
    private Marker _endPointMarker;
    private Map _mapRef;
    private Polyline _polyline;
    private readonly List<Marker> _extraPointsMarkers = new List<Marker>();
    private SelectionState _selectionState = SelectionState.None;
    private HubConnection _connection;
    private Marker _taxiMarker;
    public string SelectedCategory { get; set; } = string.Empty;
    public bool CanAddMarker { get; set; }

    [Inject] public IIconFactory IconFactory { get; init; }

    [Inject] public IHttpService HttpService { get; init; }

    [Inject] public ILocalStorageService LocalStorageService { get; init; }

    public async Task OnMarkerAddedAsync(Marker marker)
    {
        CanAddMarker = false;

        switch (_selectionState)
        {
            case SelectionState.StartPoint:
                if (_startPointMarker is not null)
                {
                    await _startPointMarker.DeleteAsync();
                }

                _startPointMarker = marker;
                break;

            case SelectionState.EndPoint:
                if (_endPointMarker is not null)
                {
                    await _endPointMarker?.DeleteAsync();
                }

                _endPointMarker = marker;
                break;

            case SelectionState.ExtraPoint:
                _extraPointsMarkers.Add(marker);
                break;
        }

        _selectionState = SelectionState.None;
        await UpdateLine();
        StateHasChanged();
    }

    public void EnableStartPointSelection()
    {
        _markerConfig.IconUrl = _bebraGreenPath;
        _markerConfig.IconSize = _bebraSize;
        _markerConfig.IconAnchor = _bebraAnchor;
        _selectionState = SelectionState.StartPoint;
        CanAddMarker = true;
    }

    public void EnableEndPointSelection()
    {
        Console.WriteLine(SelectedCategory);
        _markerConfig.IconUrl = _bebraRedPath;
        _markerConfig.IconSize = _bebraSize;
        _markerConfig.IconAnchor = _bebraAnchor;
        _selectionState = SelectionState.EndPoint;
        CanAddMarker = true;
    }

    public void EnableExtraPointSelection()
    {
        _markerConfig.IconUrl = _bebraBluePath;
        _markerConfig.IconSize = _bebraSize;
        _markerConfig.IconAnchor = _bebraAnchor;
        _selectionState = SelectionState.ExtraPoint;
        CanAddMarker = true;
    }

    public async Task RemoveExtraPointAsync(int pointNumber)
    {
        await _extraPointsMarkers[pointNumber].DeleteAsync();
        _extraPointsMarkers.RemoveAt(pointNumber);
        await UpdateLine();
    }

    public async Task StartDriverSearchAsync()
    {
        if (_startPointMarker is null || _endPointMarker is null || string.IsNullOrEmpty(SelectedCategory))
        {
            return;
        }

        var command = new CreateRide.ExternalCommand(
            new LocationDto(
                new AddressDto(_startPointMarker.Address),
                new CoordinateDto(_startPointMarker.Coordinates.Latitude, _startPointMarker.Coordinates.Longitude)),
            new LocationDto(
                new AddressDto(_endPointMarker.Address),
                new CoordinateDto(_endPointMarker.Coordinates.Latitude, _endPointMarker.Coordinates.Longitude)),
            SelectedCategory,
            "Cash",
            _extraPointsMarkers.Select(p => new LocationDto(
                                           new AddressDto(p.Address),
                                           new CoordinateDto(p.Coordinates.Latitude, p.Coordinates.Longitude)))
                               .ToList());

        CreateRide.Response result = await HttpService.PostAsync<CreateRide.Response>("/api/rides/create", command);

        UserToken token = await LocalStorageService.GetItemAsync<UserToken>("user");

        _connection = new HubConnectionBuilder()
                      .WithUrl(
                          "https://localhost:5001/hubs/client",
                          options =>
                          {
                              options.Headers["Authorization"] = token.Token;
                              options.AccessTokenProvider = () => Task.FromResult(token.Token);
                          })
                      .Build();

        await _connection.StartAsync();

        SpokeRegistration registration = _connection.RegisterSpoke<IUserClient>(
            new UserClient(DriverCoordinatesUpdated, DriverFound, DriverArrived, RideStarted, RideFinished));
    }

    protected override async Task OnInitializedAsync()
    {
        _taxiCategories = await HttpService.GetAsync<List<string>>("/cars/categories");
    }

    private async Task UpdateLine()
    {
        if (_polyline is not null)
        {
            await _polyline?.DeleteAsync();
        }

        var points = new List<MapPoint>(_extraPointsMarkers.Select(p => p.Coordinates));

        if (_startPointMarker is not null)
        {
            points.Insert(0, _startPointMarker.Coordinates);
        }

        if (_endPointMarker is not null)
        {
            points.Add(_endPointMarker.Coordinates);
        }

        _polyline = new Polyline(points, await _mapRef.CreatePolyline(points));
    }

    private void DriverCoordinatesUpdated(CoordinateDto coordinateDto)
    {
        _taxiMarker?.DeleteAsync().GetAwaiter().GetResult();
        _markerConfig.IconUrl = _bebraTaxi;

        _taxiMarker = _mapRef.CreateMarker(new MapPoint(coordinateDto.Latitude, coordinateDto.Longitude)).GetAwaiter()
                             .GetResult();
    }

    private void DriverFound()
    {
    }

    private void DriverArrived()
    {
    }

    private void RideStarted()
    {
    }

    private void RideFinished()
    {
    }
}
#pragma warning disable SA1402
public class CarCategory
#pragma warning restore SA1402
{
    public string Value { get; set; }
}