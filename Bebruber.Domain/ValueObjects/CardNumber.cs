using Bebruber.Domain.Tools;

namespace Bebruber.Domain.ValueObjects;

public class CardNumber : ValueObject<CardNumber>
{
    public CardNumber(string value)
    {
        // TODO: Card number validation logic
        Value = value;
    }

    public string Value { get; private init; }

    public override int GetHashCode()
        => Value.GetHashCode();

    protected override bool EqualTo(CardNumber other)
        => other.Value.Equals(Value);
}