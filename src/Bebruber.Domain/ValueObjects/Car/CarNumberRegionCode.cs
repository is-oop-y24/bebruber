using System.Text.RegularExpressions;
using Bebruber.Domain.Tools;
using Bebruber.Domain.ValueObjects.Exceptions;

namespace Bebruber.Domain.ValueObjects.Car;

public class CarNumberRegionCode : ValueOf<string, CarNumberRegionCode>
{
    public CarNumberRegionCode(string value)
        : base(value, Regex.IsMatch, new InvalidRegionCodeException(value)) { }

    protected CarNumberRegionCode() { }

    public static Regex Regex { get; } = new Regex("[0-9]{2,3}");
}