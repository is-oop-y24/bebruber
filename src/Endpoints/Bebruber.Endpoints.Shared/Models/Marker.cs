using System.Threading.Tasks;
using FisSst.BlazorMaps;

namespace Bebruber.Endpoints.Shared.Models
{
    public class Marker
    {
        private readonly FisSst.BlazorMaps.Marker _marker;
        public Marker(MapPoint coordinates, FisSst.BlazorMaps.Marker marker)
        {
            _marker = marker;
            Address = null;
            Coordinates = coordinates;
        }

        public Marker(MapPoint coordinates, string address, FisSst.BlazorMaps.Marker marker)
        {
            _marker = marker;
            Address = address;
            Coordinates = coordinates;
        }

        public string Address { get; }
        public MapPoint Coordinates { get; }
        public string Tooltip { get; }

        public async Task SetTooltipAsync(string tooltip)
        {
            await _marker.BindTooltip(tooltip);
        }

        public async Task RemoveTooltipAsync()
        {
            await _marker.UnbindTooltip();
        }

        public async Task DeleteAsync()
        {
            await _marker.Remove();
        }
    }
}