using Bebruber.Common.Dto;
using Bebruber.Domain.ValueObjects.Ride;

namespace Bebruber.Application.Extensions;

public static class LocationDtoExtensions
{
    public static Location ToLocation(this LocationDto locationDto)
        => new Location(
            new Address(
                locationDto.Address.Country,
                locationDto.Address.City,
                locationDto.Address.Street,
                locationDto.Address.City),
            new Coordinate(locationDto.Latitude, locationDto.Longitude));
}