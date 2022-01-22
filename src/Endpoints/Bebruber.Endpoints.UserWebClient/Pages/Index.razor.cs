using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FisSst.BlazorMaps;
using Microsoft.AspNetCore.Components;
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
        private SelectionState SelectionState = SelectionState.None;

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
                    var iconOptions = new IconOptions()
                    {
                        IconUrl = "/_content/Bebruber.Endpoints.Shared/img/bebra-red.png"
                    };

                    Icon icon = await IconFactory.Create(iconOptions);
                    _startPointMarker = marker;
                    await marker.SetIcon(icon);
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
            SelectionState = SelectionState.StartPoint;
            CanAddMarker = true;
        }

        public void EnableEndPointSelection()
        {
            SelectionState = SelectionState.EndPoint;
            CanAddMarker = true;
        }

        public void EnableExtraPointSelection()
        {
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