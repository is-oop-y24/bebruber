using System.Collections.Generic;
using Bebruber.Domain.Tools;

namespace Bebruber.Domain.ValueObjects;

public class Address : ValueObject<Address>
{
    public Address(string country, string street, int houseNumber, int apartmentNumber)
    {
        Country = country;
        Street = street;
        HouseNumber = houseNumber;
        ApartmentNumber = apartmentNumber;
    }

    public string Country { get; private init; }
    public string Street { get; private init; }
    public int HouseNumber { get; private init; }
    public int ApartmentNumber { get; private init; }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Country;
        yield return Street;
        yield return HouseNumber;
        yield return ApartmentNumber;
    }
}