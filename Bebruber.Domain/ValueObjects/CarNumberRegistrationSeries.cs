using System.Text.RegularExpressions;
using Bebruber.Domain.Tools;
using Bebruber.Domain.ValueObjects.Exceptions;

namespace Bebruber.Domain.ValueObjects;

public class CarNumberRegistrationSeries : ValueOf<string, CarNumberRegistrationSeries>
{
    public CarNumberRegistrationSeries(string value)
        : base(value.ToUpper(), Regex.IsMatch, new InvalidRegistrationSeriesException(value)) { }

    public static Regex Regex { get; } = new Regex("[а-я]{1}[0-9]{3}[а-я]{2}", RegexOptions.IgnoreCase | RegexOptions.Compiled);
}