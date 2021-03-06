using System.Text.RegularExpressions;
using Bebruber.Domain.Tools;
using Bebruber.Domain.ValueObjects.Exceptions;

namespace Bebruber.Domain.ValueObjects.Card;

public class CvvCode : ValueOf<string, CvvCode>
{
    public CvvCode(string value)
        : base(value, Regex.IsMatch, new InvalidCvvCodeException(value)) { }

    protected CvvCode() { }

    public static Regex Regex { get; } = new Regex(@"[0-9]{3}", RegexOptions.Compiled);
}