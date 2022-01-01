using System.Drawing;
using Bebruber.Domain.Tools;

namespace Bebruber.Domain.Enumerations;

public class CarColor : Enumeration<Color, CarColor>
{
    protected CarColor(string name, Color value)
        : base(name, value) { }

    public static CarColor White { get; } = new CarColor(nameof(White), Color.White);
    public static CarColor Black { get; } = new CarColor(nameof(Black), Color.Black);
}