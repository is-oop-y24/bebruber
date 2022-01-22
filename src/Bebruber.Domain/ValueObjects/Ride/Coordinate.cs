using System;
using System.Collections.Generic;
using Bebruber.Domain.Tools;

namespace Bebruber.Domain.ValueObjects.Ride;

public class Coordinate : ValueObject<Coordinate>
{
    public Coordinate(double latitude, double longitude)
    {
        Latitude = latitude;
        Longitude = longitude;
    }

    public double Latitude { get; protected init; }
    public double Longitude { get; protected init; }

    public override string ToString()
        => $"Latitude: {Latitude}, Longitude: {Longitude}";

    public double DistanceBetween(Coordinate coordinate)
        => Math.Acos((Math.Sin(Latitude) * Math.Sin(coordinate.Latitude)) +
                     (Math.Cos(Latitude) * Math.Cos(coordinate.Latitude) *
                      Math.Cos(coordinate.Longitude - Longitude))) * 6371;

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Latitude;
        yield return Longitude;
    }
}