using Bebruber.Domain.Tools;

namespace Bebruber.Domain.Enumerations;

public class CarCategory : Enumeration<int, CarCategory>
{
    protected CarCategory(string name, int value)
        : base(name, value) { }

    public CarCategory Economy { get; } = new CarCategory(nameof(Economy), 1);
    public CarCategory Comfort { get; } = new CarCategory(nameof(Comfort), 2);
    public CarCategory Business { get; } = new CarCategory(nameof(Business), 3);
}