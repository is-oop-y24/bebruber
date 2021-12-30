using Bebruber.Domain.Tools;

namespace Bebruber.Domain.ValueObjects.Exceptions;

public class InvalidSeriesException : BebruberException
{
    public InvalidSeriesException(string value)
        : base($"Invalid {nameof(CarNumberSeries)}, value: {value}") { }
}