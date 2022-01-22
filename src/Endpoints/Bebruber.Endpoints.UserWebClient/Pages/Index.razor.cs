using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FisSst.BlazorMaps;
using Microsoft.AspNetCore.Components;
using MarkerConfig = Bebruber.Endpoints.Shared.Models.MakerConfig;
using Marker = Bebruber.Endpoints.Shared.Models.Marker;

namespace Bebruber.Endpoints.UserWebClient.Pages
{
    public enum SelectionState
    {
        None = 0,
        StartPoint,
        EndPoint,
        ExtraPoint,
    }

    public partial class Index
    { 
        [Inject] private IIconFactory IconFactory { get; init; }
        public bool CanAddMarker { get; set; } = false;
        public SelectionState SelectionState = SelectionState.None;

        private MarkerConfig _markerConfig = new MarkerConfig();
        private Marker _startPointMarker;
        private Marker _endPointMarker;
        private readonly List<Marker> _extraPointsMarkers = new List<Marker>();

        public async Task OnMarkerAddedAsync(Marker marker)
        {
            CanAddMarker = false;

            switch (SelectionState)
            {
                case SelectionState.StartPoint:
                    if (_startPointMarker is not null)
                        await _startPointMarker.DeleteAsync();
                    _startPointMarker = marker;
                    break;
                case SelectionState.EndPoint:
                    if (_endPointMarker is not null)
                        await _endPointMarker?.DeleteAsync();
                    _endPointMarker = marker;
                    break;
                case SelectionState.ExtraPoint:
                    _extraPointsMarkers.Add(marker);
                    break;
            };

            SelectionState = SelectionState.None;
            StateHasChanged();
        }

        public void EnableStartPointSelection()
        {
            _markerConfig.IconUrl = "/_content/Bebruber.Endpoints.Shared/img/bebra-green.png";
            _markerConfig.IconSize = new Point(54/2, 95/2);
            _markerConfig.IconAnchor = new Point(54/4, 95/2);
            SelectionState = SelectionState.StartPoint;
            CanAddMarker = true;
        }

        public void EnableEndPointSelection()
        {
            _markerConfig.IconUrl = "/_content/Bebruber.Endpoints.Shared/img/bebra-red.png";
            _markerConfig.IconSize = new Point(54/2, 95/2);
            _markerConfig.IconAnchor = new Point(54/4, 95/2);
            SelectionState = SelectionState.EndPoint;
            CanAddMarker = true;
        }

        public void EnableExtraPointSelection()
        {
            _markerConfig.IconUrl = "/_content/Bebruber.Endpoints.Shared/img/bebra-blue.png";
            _markerConfig.IconSize = new Point(54/2, 95/2);
            _markerConfig.IconAnchor = new Point(54/4, 95/2);
            SelectionState = SelectionState.ExtraPoint;
            CanAddMarker = true;
        }

        public async Task RemoveExtraPointAsync(int pointNumber)
        {
            Console.WriteLine(pointNumber);
            await _extraPointsMarkers[pointNumber].DeleteAsync();
            _extraPointsMarkers.RemoveAt(pointNumber);
        }
    }
}