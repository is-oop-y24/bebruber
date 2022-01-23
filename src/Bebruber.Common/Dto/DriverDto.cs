namespace Bebruber.Common.Dto;

public record DriverDto(
    string Name,
    double Rating,
    CarDto Car
);