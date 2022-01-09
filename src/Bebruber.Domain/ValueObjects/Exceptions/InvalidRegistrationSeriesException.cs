using Bebruber.Domain.Tools;
using Bebruber.Domain.ValueObjects.Car;

namespace Bebruber.Domain.ValueObjects.Exceptions;

public class InvalidRegistrationSeriesException : BebruberException
{
    public InvalidRegistrationSeriesException(string value)
        : base($"Invalid {nameof(CarNumberRegistrationSeries)}, value: {value}") { }
}