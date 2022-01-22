namespace Bebruber.Common.Dto;

public record LocationDto(
    AddressDto Address,
    double Latitude,
    double Longitude
);