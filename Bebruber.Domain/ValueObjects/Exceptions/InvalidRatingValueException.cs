using Bebruber.Domain.Tools;

namespace Bebruber.Domain.ValueObjects.Exceptions;

public class InvalidRatingValueException : BebruberException
{
    public InvalidRatingValueException(double value)
        : base($"Invalid value of rating, value: {value}") { }
}