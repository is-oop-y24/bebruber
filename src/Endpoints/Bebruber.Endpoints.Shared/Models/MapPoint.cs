using FisSst.BlazorMaps;

namespace Bebruber.Endpoints.Shared.Models
{
    public class MapPoint
    {
        public double Latitude { get; }
        public double Longitude { get; }

        public MapPoint(double latitude, double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }

        public MapPoint(LatLng latLng)
        {
            Latitude = latLng.Lat;
            Longitude = latLng.Lng;
        }

        internal LatLng ToLatLng() => new LatLng(Latitude, Longitude);

        public override string ToString() => $"{Latitude} {Longitude}";
    }
}