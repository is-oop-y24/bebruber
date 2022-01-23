using System.Runtime.CompilerServices;
using Bebruber.Domain.Tools;
using Bebruber.Domain.ValueObjects.Exceptions;

namespace Bebruber.Domain.ValueObjects.Ride;

public class Roubles : ValueOf<decimal, Roubles>
{
    public Roubles(decimal value)
        : base(value, d => d >= 0, new InvalidRoublesValueException(value)) { }

    protected Roubles() { }

    public static Roubles operator +(Roubles a, Roubles b)
    {
        return new Roubles(a.Value + b.Value);
    }

    public static Roubles operator *(Roubles a, double b)
    {
        return new Roubles(a.Value * (decimal)b);
    }
}