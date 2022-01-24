using Bebruber.Domain.Enumerations;
using Bebruber.Domain.Tools;
using Bebruber.Domain.ValueObjects.Car;
using Bebruber.Utility.Extensions;

namespace Bebruber.Domain.Entities;

public class Car : Entity<Car>
{
    public Car(CarBrand brand, CarName name, CarColor color, CarCategory category, CarNumber carNumber)
    {
        Brand = brand.ThrowIfNull();
        Name = name.ThrowIfNull();
        Color = color.ThrowIfNull();
        Category = category.ThrowIfNull();
        Number = carNumber.ThrowIfNull();
    }

    private Car() { }

    public virtual CarNumber Number { get; set; }
    public virtual CarBrand Brand { get; private init; }
    public virtual CarName Name { get; private init; }
    public virtual CarColor Color { get; private init; }
    public virtual CarCategory Category { get; private init; }

    public override string ToString()
        => $"[{Id}] [{Number}] {Category} {Brand} {Name} ({Color})";
}