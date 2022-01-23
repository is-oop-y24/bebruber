namespace Bebruber.Endpoints.Shared.Models;

public class Address
{
    public string HouseNumber { get; set; }
    public string Road { get; set; }
    public string Suburb { get; set; }
    public string City { get; set; }
    public string StateDistrict { get; set; }
    public string State { get; set; }
    public string Postcode { get; set; }
    public string Country { get; set; }
    public string CountryCode { get; set; }
}

public class LocationInfo
{
    public string PlaceId { get; set; }
    public string Licence { get; set; }
    public string OsmType { get; set; }
    public string OsmId { get; set; }
    public string Lat { get; set; }
    public string Lon { get; set; }
    public string DisplayName { get; set; }
    public Address Address { get; set; }
}