using Bebruber.Domain.Tools;
using Bebruber.Domain.ValueObjects.Ride;

namespace Bebruber.Domain.ValueObjects.Exceptions;

public class InvalidRoublesValueException : BebruberException
{
    public InvalidRoublesValueException(decimal value)
        : base($"Invalid {nameof(Roubles)} value: {value}") { }
}