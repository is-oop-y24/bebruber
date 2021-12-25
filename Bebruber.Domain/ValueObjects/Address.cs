using System;
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

    public override int GetHashCode()
        => HashCode.Combine(Country, Street, HouseNumber, ApartmentNumber);

    protected override bool EqualTo(Address other)
        => other.Country.Equals(Country) &&
           other.Street.Equals(Street) &&
           other.HouseNumber.Equals(HouseNumber) &&
           other.ApartmentNumber.Equals(ApartmentNumber);
}