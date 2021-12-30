using System.Text.RegularExpressions;
using Bebruber.Domain.Tools;
using Bebruber.Domain.ValueObjects.Exceptions;

namespace Bebruber.Domain.ValueObjects;

public class CarNumberRegistrationNumber : ValueOf<string, CarNumberRegistrationNumber>
{
    public CarNumberRegistrationNumber(string value)
        : base(value, Regex.IsMatch, new InvalidRegistrationNumberException(value)) { }

    public static Regex Regex { get; } = new Regex($"[0-9]{3}");
}