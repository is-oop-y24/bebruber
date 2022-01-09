using Bebruber.Domain.Tools;
using Bebruber.Domain.ValueObjects.Card;

namespace Bebruber.Domain.ValueObjects.Exceptions;

public class InvalidCardNumberException : BebruberException
{
    public InvalidCardNumberException(string value)
        : base($"Invalid {nameof(CardNumber)}, value: {value}") { }
}