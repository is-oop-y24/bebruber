using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Channels;
using Bebruber.Domain.Tools;
using Bebruber.Domain.ValueObjects.Exceptions;

namespace Bebruber.Domain.ValueObjects.Card;

public class CardNumber : ValueOf<string, CardNumber>
{
    public CardNumber(string value)
        : base(value, Regex.IsMatch, new InvalidCardNumberException(value)) { }

    public static Regex Regex { get; } = new Regex("[0-9]{16}", RegexOptions.Compiled);

    public override string ToString()
    {
        ReadOnlySpan<char> startSpan = Value.AsSpan().Slice(start: 0, length: 4);
        ReadOnlySpan<char> endSpan = Value.AsSpan().Slice(start: 12, length: 4);
        return new StringBuilder(16).Append(startSpan).Insert(4, "*", 8).Append(endSpan).ToString();
    }
}