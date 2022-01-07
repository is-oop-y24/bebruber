using Bebruber.Domain.Tools;
using Bebruber.Utility.Extensions;

namespace Bebruber.Domain.ValueObjects;

public class CarBrand : ValueOf<string, CarBrand>
{
    public CarBrand(string value)
        : base(value.ThrowIfNull()) { }
}