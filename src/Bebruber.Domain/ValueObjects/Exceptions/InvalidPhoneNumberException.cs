using Bebruber.Domain.Tools;

namespace Bebruber.Domain.ValueObjects.Exceptions;

public class InvalidPhoneNumberException : BebruberException
{
    public InvalidPhoneNumberException(string value)
        : base($"Invalid phone number, value: {value}")
    { }
}