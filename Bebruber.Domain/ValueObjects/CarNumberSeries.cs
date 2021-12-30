using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Bebruber.Domain.Tools;
using Bebruber.Domain.ValueObjects.Exceptions;

namespace Bebruber.Domain.ValueObjects;

public class CarNumberSeries : ValueOf<string, CarNumberSeries>
{
    public CarNumberSeries(string value)
        : base(value.ToUpper(), Regex.IsMatch, new InvalidSeriesException(value)) { }

    public static Regex Regex { get; } = new Regex("[а-я]{3}", RegexOptions.IgnoreCase | RegexOptions.Compiled);

    public char FirstLetter => Value[0];
    public ReadOnlySpan<char> SecondLetters => Value.AsSpan(1, 2);
}