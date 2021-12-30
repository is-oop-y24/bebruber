using System.Collections.Generic;
using Bebruber.Domain.Tools;
using Bebruber.Utility.Extensions;

namespace Bebruber.Domain.ValueObjects;

public class CarNumber : ValueObject<CardNumber>
{
    public CarNumber(CarNumberSeries series, CarNumberRegistrationNumber registrationNumber, CarNumberRegionCode regionCode)
    {
        Series = series.ThrowIfNull();
        RegistrationNumber = registrationNumber.ThrowIfNull();
        RegionCode = regionCode.ThrowIfNull();
    }

    public CarNumberSeries Series { get; private init; }
    public CarNumberRegistrationNumber RegistrationNumber { get; private init; }
    public CarNumberRegionCode RegionCode { get; private init; }

    public override string ToString()
        => $"{Series.FirstLetter}{RegistrationNumber}{Series.SecondLetters} {RegionCode}";

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Series;
        yield return RegistrationNumber;
        yield return RegionCode;
    }
}