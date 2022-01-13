namespace Bebruber.Endpoints.Shared.Models;

public class Address
{
    public string house_number { get; set; }
    public string road { get; set; }
    public string suburb { get; set; }
    public string city { get; set; }
    public string state_district { get; set; }
    public string state { get; set; }
    public string postcode { get; set; }
    public string country { get; set; }
    public string country_code { get; set; }
}

public class LocationInfo
{
    public string place_id { get; set; }
    public string licence { get; set; }
    public string osm_type { get; set; }
    public string osm_id { get; set; }
    public string lat { get; set; }
    public string lon { get; set; }
    public string display_name { get; set; }
    public Address address { get; set; }
}