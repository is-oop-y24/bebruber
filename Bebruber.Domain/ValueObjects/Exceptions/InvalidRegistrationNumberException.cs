using Bebruber.Domain.Tools;

namespace Bebruber.Domain.ValueObjects.Exceptions;

public class InvalidRegistrationNumberException : BebruberException
{
    public InvalidRegistrationNumberException(string value)
        : base($"Invalid {nameof(CarNumberRegistrationNumber)}, value: {value}") { }
}