using System.Collections.Generic;
using System.Linq;
using Bebruber.Domain.Tools;
using Bebruber.Domain.ValueObjects.Ride;
using Bebruber.Utility.Extensions;

namespace Bebruber.Domain.Entities;

public class RideEntry : Entity<RideEntry>
{
    public RideEntry(Location origin, Location destination, IReadOnlyCollection<Location> intermediatePoints)
    {
        Origin = origin.ThrowIfNull();
        Destination = destination.ThrowIfNull();
        IntermediatePoints = intermediatePoints.ThrowIfNull().ToList();
    }

    public Location Origin { get; }
    public Location Destination { get; }
    public IReadOnlyCollection<Location> IntermediatePoints { get; }
}