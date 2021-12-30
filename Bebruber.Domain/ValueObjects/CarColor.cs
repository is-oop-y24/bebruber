using System.Collections.Generic;
using System.Drawing;
using Bebruber.Domain.Tools;

namespace Bebruber.Domain.ValueObjects;

public class CarColor : ValueObject<CarColor>
{
    public CarColor(string name, Color color)
    {
        Name = name;
        Color = color;
    }

    public string Name { get; private init; }
    public Color Color { get; private init; }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Name;
        yield return Color;
    }
}