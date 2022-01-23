using Bebruber.Common.Dto;
using Bebruber.Domain.ValueObjects.Ride;

namespace Bebruber.Application.Common.Extensions;

public static class CoordinateDtoExtensions
{
    public static CoordinateDto ToDto(this Coordinate coordinate)
        => new CoordinateDto(coordinate.Latitude, coordinate.Longitude);

    public static Coordinate ToCoordinate(this CoordinateDto coordinateDto)
        => new Coordinate(coordinateDto.Latitude, coordinateDto.Longitude);
}