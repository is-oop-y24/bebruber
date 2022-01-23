using System.Collections.Generic;
using Bebruber.Domain.Tools;
using Bebruber.Utility.Extensions;

namespace Bebruber.Domain.ValueObjects.Ride;

public class Location : ValueObject<Location>
{
    public Location(Address address, Coordinate coordinate)
    {
        Address = address.ThrowIfNull();
        Coordinate = coordinate.ThrowIfNull();
    }

    protected Location() { }

    public Address Address { get; protected init; }
    public Coordinate Coordinate { get; protected init; }

    public override string ToString()
        => $"{Address}, {Coordinate}";

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Address;
        yield return Coordinate;
    }
}