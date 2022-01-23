using Bebruber.Domain.Tools;
using Bebruber.Utility.Extensions;

namespace Bebruber.Domain.ValueObjects.Car;

public class CarName : ValueOf<string, CarName>
{
    public CarName(string value)
        : base(value.ThrowIfNull()) { }

    protected CarName() { }
}