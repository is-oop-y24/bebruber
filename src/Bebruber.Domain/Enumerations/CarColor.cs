using System.Drawing;
using Bebruber.Domain.Enumerations.Exceptions;
using Bebruber.Domain.Tools;

namespace Bebruber.Domain.Enumerations;

public class CarColor : Enumeration<Color, CarColor>
{
    protected CarColor(string name, Color value)
        : base(name, value) { }

    protected CarColor() { }

    public static CarColor White => new CarColor(nameof(White), Color.White);
    public static CarColor Black => new CarColor(nameof(Black), Color.Black);

    public static CarColor Parse(string name)
    {
        return name switch
        {
            nameof(White) => White,
            nameof(Black) => Black,
            _ => throw new EnumerationParseException<string>(nameof(CarColor), name),
        };
    }
}