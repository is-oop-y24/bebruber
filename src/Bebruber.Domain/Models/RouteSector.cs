using Bebruber.Domain.ValueObjects.Ride;
using Bebruber.Utility.Extensions;

namespace Bebruber.Domain.Models;

public class RouteSector
{
    public RouteSector(Coordinate begin, Coordinate end, LoadLevel loadLevel)
    {
        Begin = begin.ThrowIfNull();
        End = end.ThrowIfNull();
        LoadLevel = loadLevel;
    }

    public double Length => Begin.DistanceBetween(End);

    public Coordinate Begin { get; }
    public Coordinate End { get; }
    public LoadLevel LoadLevel { get; }
}