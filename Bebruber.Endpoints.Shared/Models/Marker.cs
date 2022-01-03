using System.Threading.Tasks;

namespace Bebruber.Endpoints.Shared.Models
{
    public class Marker
    {
        private FisSst.BlazorMaps.Marker _marker;

        public MapPoint Coordinates { get; }
        public string Tooltip { get; }

        public Marker(MapPoint coordinates, FisSst.BlazorMaps.Marker marker)
        {
            _marker = marker;
            Coordinates = coordinates;
        }

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