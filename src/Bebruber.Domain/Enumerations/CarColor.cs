using System;
using System.Drawing;
using Bebruber.Domain.Tools;

namespace Bebruber.Domain.Enumerations;

public class CarColor : Enumeration<Color, CarColor>
{
    protected CarColor(string name, Color value)
        : base(name, value) { }

    public static CarColor White { get; } = new CarColor(nameof(White), Color.White);
    public static CarColor Black { get; } = new CarColor(nameof(Black), Color.Black);

    public static CarColor Parse(string value)
    {
        switch (value.ToLower())
        {
            case "white":
                return White;
            case "black":
                return Black;
            default:
                throw new ArgumentOutOfRangeException(value);
        }
    }
}