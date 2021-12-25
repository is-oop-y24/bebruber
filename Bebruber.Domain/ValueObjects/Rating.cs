using System;
using Bebruber.Domain.Tools;
using Bebruber.Domain.ValueObjects.Exceptions;

namespace Bebruber.Domain.ValueObjects;

public class Rating : ValueObject<Rating>
{
    private const int NumberOfDigits = 2;

    public Rating(double value)
    {
        if (value is < 0 or > 10)
            throw new InvalidRatingValueException(value);

        Value = Math.Round(value, NumberOfDigits);
    }

    public double Value { get; private init; }

    public override int GetHashCode()
        => Value.GetHashCode();

    protected override bool EqualTo(Rating other)
        => other.Value.Equals(Value);
}