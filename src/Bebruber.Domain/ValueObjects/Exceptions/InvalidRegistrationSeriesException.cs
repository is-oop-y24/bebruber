using Bebruber.Domain.Tools;

namespace Bebruber.Domain.ValueObjects.Exceptions;

public class InvalidRegistrationSeriesException : BebruberException
{
    public InvalidRegistrationSeriesException(string value)
        : base($"Invalid {nameof(CarNumberRegistrationSeries)}, value: {value}") { }
}