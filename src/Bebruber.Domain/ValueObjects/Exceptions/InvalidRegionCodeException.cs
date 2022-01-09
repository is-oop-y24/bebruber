using Bebruber.Domain.Tools;
using Bebruber.Domain.ValueObjects.Car;

namespace Bebruber.Domain.ValueObjects.Exceptions;

public class InvalidRegionCodeException : BebruberException
{
    public InvalidRegionCodeException(string value)
        : base($"Invalid {nameof(CarNumberRegionCode)}, value: {value}") { }
}