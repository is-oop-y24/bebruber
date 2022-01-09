using System;
using System.Collections.Generic;
using System.Linq;
using Bebruber.Domain.ValueObjects.Ride;
using Bebruber.Utility.Extensions;

namespace Bebruber.Domain.Models;

public class RideEntry
{
    public RideEntry(Location origin, Location destination, IReadOnlyCollection<Location> intermediatePoints)
    {
        Id = Guid.NewGuid();
        Origin = origin.ThrowIfNull();
        Destination = destination.ThrowIfNull();
        IntermediatePoints = intermediatePoints.ThrowIfNull().ToList();
    }

    public Guid Id { get; }
    public Location Origin { get; }
    public Location Destination { get; }
    public IReadOnlyCollection<Location> IntermediatePoints { get; }
}