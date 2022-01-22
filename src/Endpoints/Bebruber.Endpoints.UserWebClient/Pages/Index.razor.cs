using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bebruber.Endpoints.Shared.Models;
using FisSst.BlazorMaps;
using Microsoft.AspNetCore.Components;
using MarkerConfig = Bebruber.Endpoints.Shared.Models.MakerConfig;
using Marker = Bebruber.Endpoints.Shared.Models.Marker;
using Polyline = Bebruber.Endpoints.Shared.Models.Polyline;

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
        [Inject] public IIconFactory IconFactory { get; init; }
        public bool CanAddMarker { get; set; } = false;
        public SelectionState SelectionState = SelectionState.None;

        private MarkerConfig _markerConfig = new MarkerConfig();
        private Marker _startPointMarker;
        private Marker _endPointMarker;
        private readonly List<Marker> _extraPointsMarkers = new List<Marker>();
        private Bebruber.Endpoints.Shared.Components.Map _mapRef;
        private Polyline _polyline;

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
            await UpdateLine();
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
            await _extraPointsMarkers[pointNumber].DeleteAsync();
            _extraPointsMarkers.RemoveAt(pointNumber);
            await UpdateLine();
        }

        private async Task UpdateLine()
        {
            if (_polyline is not null)
                await _polyline?.DeleteAsync();
            var points = new List<MapPoint>(_extraPointsMarkers.Select(p => p.Coordinates));
            if(_startPointMarker is not null)
                points.Insert(0 ,_startPointMarker.Coordinates);
            if(_endPointMarker is not null)
                points.Add(_endPointMarker.Coordinates);
            var latLngs = new List<LatLng>(new List<LatLng>(points.Select(p => new LatLng(p.Latitude, p.Longitude))));
            _polyline = new Polyline(points, await _mapRef.CreatePolyline(latLngs));
        }
    }
}