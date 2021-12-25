using Bebruber.Domain.Tools;

namespace Bebruber.Domain.ValueObjects.Exceptions;

public class InvalidCvvCodeException : BebruberException
{
    public InvalidCvvCodeException(string value)
        : base($"Invalid CVV code, value: {value}") { }
}