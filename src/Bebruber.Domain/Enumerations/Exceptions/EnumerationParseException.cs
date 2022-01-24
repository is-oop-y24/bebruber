using Bebruber.Domain.Tools;

namespace Bebruber.Domain.Enumerations.Exceptions;

public class EnumerationParseException<T> : BebruberException
{
    public EnumerationParseException(string typeName, T value)
        : base($"Value {value} is not a proper value for {typeName}") { }
}