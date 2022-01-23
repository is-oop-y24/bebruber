using FisSst.BlazorMaps;

namespace Bebruber.Endpoints.Shared.Models
{
    public class MapPoint
    {
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

        public double Latitude { get; }
        public double Longitude { get; }

        public override string ToString() => $"{Latitude} {Longitude}";

        internal LatLng ToLatLng() => new LatLng(Latitude, Longitude);
    }
}