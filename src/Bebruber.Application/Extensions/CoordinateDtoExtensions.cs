using Bebruber.Common.Dto;
using Bebruber.Domain.ValueObjects.Ride;

namespace Bebruber.Application.Extensions;

public static class CoordinateDtoExtensions
{
    public static Coordinate ToCoordinate(this CoordinateDto coordinateDto)
        => new Coordinate(coordinateDto.Latitude, coordinateDto.Longitude);
}