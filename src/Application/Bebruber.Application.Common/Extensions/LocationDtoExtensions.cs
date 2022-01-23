using Bebruber.Common.Dto;
using Bebruber.Domain.ValueObjects.Ride;

namespace Bebruber.Application.Common.Extensions;

public static class LocationDtoExtensions
{
    public static LocationDto ToDto(this Location location)
        => new LocationDto(location.Address.ToDto(), location.Coordinate.ToDto());
    
    public static Location ToLocation(this LocationDto locationDto)
        => new Location(locationDto.Address.ToAddress(), locationDto.CoordinateDto.ToCoordinate());
}