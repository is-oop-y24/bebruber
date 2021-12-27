using System;
using System.Collections.Generic;
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

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }
}