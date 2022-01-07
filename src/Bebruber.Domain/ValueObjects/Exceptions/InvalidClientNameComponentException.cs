using Bebruber.Domain.Tools;

namespace Bebruber.Domain.ValueObjects.Exceptions;

public class InvalidClientNameComponentException : BebruberException
{
    public InvalidClientNameComponentException(string componentName, string? componentValue)
        : base($"Client {componentName} is invalid. Value: {componentValue}.") { }
}