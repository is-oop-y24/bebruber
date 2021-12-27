using System.Collections.Generic;
using System.Text.RegularExpressions;
using Bebruber.Domain.Tools;
using Bebruber.Domain.ValueObjects.Exceptions;

namespace Bebruber.Domain.ValueObjects;

public class CardNumber : ValueObject<CardNumber>
{
    public CardNumber(string value)
    {
        if (!Regex.IsMatch(value))
            throw new InvalidCardNumberException(value);

        Value = value;
    }

    public static Regex Regex { get; } = new Regex("[0-9]{16}", RegexOptions.Compiled);

    public string Value { get; private init; }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }
}