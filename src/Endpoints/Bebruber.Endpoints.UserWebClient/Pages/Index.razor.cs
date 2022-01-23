using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bebruber.Endpoints.Shared.Models;
using FisSst.BlazorMaps;
using Microsoft.AspNetCore.Components;
using Marker = Bebruber.Endpoints.Shared.Models.Marker;
using MarkerConfig = Bebruber.Endpoints.Shared.Models.MakerConfig;
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
        private static readonly string _bebraGreenPath = "/_content/Bebruber.Endpoints.Shared/img/bebra-green.png";
        private static readonly string _bebraRedPath = "/_content/Bebruber.Endpoints.Shared/img/bebra-red.png";
        private static readonly string _bebraBluePath = "/_content/Bebruber.Endpoints.Shared/img/bebra-blue.png";
        private static readonly Point _bebraSize = new(54 / 2, 95 / 2);
        private static readonly Point _bebraAnchor = new(54 / 4, 95 / 2);
        private MarkerConfig _markerConfig = new MarkerConfig();
        private Marker _startPointMarker;
        private Marker _endPointMarker;
        private Bebruber.Endpoints.Shared.Components.Map _mapRef;
        private Polyline _polyline;
        private List<Marker> _extraPointsMarkers = new List<Marker>();
        private SelectionState _selectionState = SelectionState.None;

        public bool CanAddMarker { get; set; } = false;

        [Inject]
        public IIconFactory IconFactory { get; init; }
        public async Task OnMarkerAddedAsync(Marker marker)
        {
            CanAddMarker = false;
            switch (_selectionState)
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

        private async Task UpdateLine()
        {
            if (_polyline is not null)
                await _polyline?.DeleteAsync();
            var points = new List<MapPoint>(_extraPointsMarkers.Select(p => p.Coordinates));
            if (_startPointMarker is not null)
                points.Insert(0, _startPointMarker.Coordinates);
            if (_endPointMarker is not null)
                points.Add(_endPointMarker.Coordinates);
            _polyline = new Polyline(points, await _mapRef.CreatePolyline(points));
        }
    }
}