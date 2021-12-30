using Bebruber.Domain.Tools;

namespace Bebruber.Domain.ValueObjects.Exceptions;

public class InvalidRegionCodeException : BebruberException
{
    public InvalidRegionCodeException(string value)
        : base($"Invalid {nameof(CarNumberRegionCode)}, value: {value}") { }
}