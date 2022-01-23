using System.Collections.Generic;
using Bebruber.Domain.Tools;
using Bebruber.Domain.ValueObjects.Card;
using Bebruber.Utility.Extensions;

namespace Bebruber.Domain.ValueObjects.Car;

public class CarNumber : ValueObject<CarNumber>
{
    public CarNumber(CarNumberRegistrationSeries series, CarNumberRegionCode regionCode)
    {
        RegistrationSeries = series.ThrowIfNull();
        RegionCode = regionCode.ThrowIfNull();
    }

    protected CarNumber() { }

    public CarNumberRegistrationSeries RegistrationSeries { get; private init; }
    public CarNumberRegionCode RegionCode { get; private init; }

    public override string ToString()
        => $"{RegistrationSeries} {RegionCode}";

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return RegistrationSeries;
        yield return RegionCode;
    }
}