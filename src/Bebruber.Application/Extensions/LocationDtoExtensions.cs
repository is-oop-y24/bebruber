using Bebruber.Common.Dto;
using Bebruber.Domain.ValueObjects.Ride;

namespace Bebruber.Application.Extensions;

public static class LocationDtoExtensions
{
    public static Location ToLocation(this LocationDto locationDto)
        => new Location(locationDto.ToAddress(), locationDto.CoordinateDto.ToCoordinate());

    public static Address ToAddress(this LocationDto locationDto)
        => new Address(
            locationDto.Address.Country,
            locationDto.Address.City,
            locationDto.Address.Street,
            locationDto.Address.City);
}