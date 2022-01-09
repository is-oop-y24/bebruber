using System.Text.RegularExpressions;
using Bebruber.Domain.Tools;
using Bebruber.Domain.ValueObjects.Exceptions;

namespace Bebruber.Domain.ValueObjects.Card;

public class CardNumber : ValueOf<string, CardNumber>
{
    public CardNumber(string value)
        : base(value, Regex.IsMatch, new InvalidCardNumberException(value)) { }

    public static Regex Regex { get; } = new Regex("[0-9]{16}", RegexOptions.Compiled);
}