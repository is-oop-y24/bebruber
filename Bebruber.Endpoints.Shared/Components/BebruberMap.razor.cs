using FisSst.BlazorMaps;

namespace Bebruber.Endpoints.Shared.Components
{
    public partial class BebruberMap
    {
        public Map MapObject { get; set; }
        public MapOptions Options { get; set; } = new MapOptions()
        {
            DivId = "Map",
            Center = new LatLng(59.956175868546254, 30.309461702204565),
            Zoom = 13,
            UrlTileLayer = "https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png",
            SubOptions = new MapSubOptions()
            {
                Attribution = "&copy; <a lhref='http://www.openstreetmap.org/copyright'>OpenStreetMap</a>",
                TileSize = 512,
                ZoomOffset = -1,
                MaxZoom = 19,
            }
        };
    }
}