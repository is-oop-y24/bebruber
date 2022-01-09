using Bebruber.Domain.Tools;
using Bebruber.Utility.Extensions;

namespace Bebruber.Domain.ValueObjects.Car;

public class CarBrand : ValueOf<string, CarBrand>
{
    public CarBrand(string value)
        : base(value.ThrowIfNull()) { }
}