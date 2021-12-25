using System.Linq;
using Bebruber.Domain.Tools;
using Bebruber.Domain.ValueObjects.Exceptions;

namespace Bebruber.Domain.ValueObjects;

public class CvvCode : ValueObject<CvvCode>
{
    public CvvCode(string value)
    {
        if (value.Length is not 3 || !value.All(char.IsDigit))
            throw new InvalidCvvCodeException(value);

        Value = value;
    }

    public string Value { get; private init; }

    public override int GetHashCode()
        => Value.GetHashCode();

    protected override bool EqualTo(CvvCode other)
        => other.Value.Equals(Value);
}