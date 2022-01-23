namespace Bebruber.Common.Dto;

public record RideDto(
    ClientDto Client,
    DriverDto Driver,
    decimal Cost,
    LocationDto Origin,
    LocationDto Destination,
    IReadOnlyCollection<LocationDto> IntermediatePoints);