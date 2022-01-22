using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Bebruber.Endpoints.Shared.Models;
using FisSst.BlazorMaps;
using Microsoft.AspNetCore.Components;
using Bebruber.Endpoints.Shared.Models;
using Newtonsoft.Json;
using Marker = Bebruber.Endpoints.Shared.Models.Marker;
using Polyline = Bebruber.Endpoints.Shared.Models.Polyline;

namespace Bebruber.Endpoints.Shared.Components
{
    public partial class Map
    {
        private FisSst.BlazorMaps.Map _mapObject;
        private MapOptions _options = new MapOptions() {
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
        [Inject] private IMarkerFactory MarkerFactory { get; set; }
        [Inject] private IIconFactory IconFactory { get; init; }
        [Inject] private IPolylineFactory PolylineFactory { get; set; }

        [Parameter] public string Height { get; set; }
        [Parameter] public bool NeedGetMarkerAddress { get; set; }

        [Parameter] public bool AddMarkerOnClick { get; set; }
        [Parameter] public MakerConfig MarkerConfig { get; set; }
        [Parameter] public Action<Marker> OnMarkerAdded { get; set; }

        private async Task MapOnClick(MouseEvent mouseEvent)
        {
            if (AddMarkerOnClick)
            {
                string markerAddress = NeedGetMarkerAddress ? await GetAddress(mouseEvent.LatLng) : null;
                FisSst.BlazorMaps.Marker marker = null;
                if (MarkerConfig.IconUrl is not "")
                {
                    var iconOptions = new IconOptions()
                    {
                        IconUrl = MarkerConfig.IconUrl,
                        IconSize =MarkerConfig.IconSize,
                        IconAnchor = MarkerConfig.IconAnchor
                    };
                    var icon = await IconFactory.Create(iconOptions);
                    var markerOptions = new MarkerOptions()
                    {
                        IconRef = icon
                    };
                    marker = await MarkerFactory.CreateAndAddToMap(mouseEvent.LatLng, _mapObject, markerOptions);
                }
                else
                {
                    marker = await MarkerFactory.CreateAndAddToMap(mouseEvent.LatLng, _mapObject);
                }
                var mapMarker = new Marker(new MapPoint(mouseEvent.LatLng), markerAddress, marker);
                OnMarkerAdded?.Invoke(mapMarker);
            }
        }

        private async Task<string> GetAddress(LatLng point)
        {
            HttpClient httpClient = new HttpClient();
            var latitudeString = point.Lat.ToString(CultureInfo.InvariantCulture);
            var longitudeString = point.Lng.ToString(CultureInfo.InvariantCulture);
            var response = await httpClient.GetAsync($"https://nominatim.openstreetmap.org/reverse?format=json&lat={latitudeString}&lon={longitudeString}");
            var stringData = await response.Content.ReadAsStringAsync();
            var location = JsonConvert.DeserializeObject<LocationInfo>(stringData);
            return location.address.road is null ? null : $"{location.address.road} {location.address.house_number}";
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            base.OnAfterRender(firstRender);
            if (!firstRender)
                return;

            // MouseEvents of Fis.Sst Map are initialized in OnAfterRender method
            // OnAfterRender of child components is called after such method of parent component
            // So we have to wait until MouseEvents are initialized
            while (true)
            {
                try
                {
                    await _mapObject.OnClick(MapOnClick);
                    break;
                }
                catch (NullReferenceException e)
                {
                    await Task.Delay(TimeSpan.FromMilliseconds(5));
                }
            }
        }

        public async Task<MapPoint> GetCenterAsync()
        {
            var center = await _mapObject.GetCenter();
            return new MapPoint(center);
        }

        public async Task<Marker> CreateMarker(MapPoint point)
        {
            var marker = await MarkerFactory.CreateAndAddToMap(point.ToLatLng(), _mapObject);
            return new Marker(point, marker);
        }

        public async Task<Polyline> CreatePolyline(ICollection<MapPoint> points)
        {
            var polyline =  await PolylineFactory.CreateAndAddToMap(points.Select(p => p.ToLatLng()), _mapObject);
            return new Polyline(points, polyline);
        }
    }
}