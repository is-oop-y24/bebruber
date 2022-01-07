using System.Collections.Generic;
using Bebruber.Domain.Tools;

namespace Bebruber.Domain.ValueObjects.Ride;

public class Coordinate : ValueObject<Coordinate>
{
    public Coordinate(double x, double y)
    {
        X = x;
        Y = y;
    }

    public double X { get; protected init; }
    public double Y { get; protected init; }

    public override string ToString()
        => $"X: {X}, Y: {Y}";

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return X;
        yield return Y;
    }
}