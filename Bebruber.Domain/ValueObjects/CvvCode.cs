using System.Collections.Generic;
using System.Text.RegularExpressions;
using Bebruber.Domain.Tools;
using Bebruber.Domain.ValueObjects.Exceptions;

namespace Bebruber.Domain.ValueObjects;

public class CvvCode : ValueObject<CvvCode>
{
    public CvvCode(string value)
    {
        if (!Regex.IsMatch(value))
            throw new InvalidCvvCodeException(value);

        Value = value;
    }

    public static Regex Regex { get; } = new Regex(@"[0-9]{3}", RegexOptions.Compiled);

    public string Value { get; private init; }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }
}