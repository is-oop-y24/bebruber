using Bebruber.Domain.Tools;

namespace Bebruber.Domain.ValueObjects.Exceptions;

public class InvalidExpirationDateComponentException : BebruberException
{
    public InvalidExpirationDateComponentException(string componentName, int componentValue)
        : base($"Invalid {componentName}, value: {componentValue}") { }
}