using Bebruber.Common.Dto;
using Bebruber.Domain.ValueObjects.Ride;

namespace Bebruber.Application.Common.Extensions;

public static class AddressDtoExtensions
{
    public static AddressDto ToDto(this Address address)
        => new AddressDto(address.HouseNumber);

    public static Address ToAddress(this AddressDto address)
        => new Address(
            string.Empty,
            string.Empty,
            string.Empty,
            address.Address);
}