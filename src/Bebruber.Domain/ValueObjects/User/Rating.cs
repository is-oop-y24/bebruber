using System;
using Bebruber.Domain.Tools;
using Bebruber.Domain.ValueObjects.Exceptions;

namespace Bebruber.Domain.ValueObjects.User;

public class Rating : ValueOf<double, Rating>
{
    private const int NumberOfDigits = 2;

    public Rating(double value)
        : base(Math.Round(value, NumberOfDigits), Validate, new InvalidRatingValueException(value)) { }

    private static bool Validate(double value)
        => value is >= 0 and < 10;
}