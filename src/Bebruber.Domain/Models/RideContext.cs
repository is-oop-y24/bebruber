using System.Collections.Generic;
using System.Linq;
using Bebruber.Domain.Entities;
using Bebruber.Domain.ValueObjects.Ride;
using Bebruber.Utility.Extensions;

namespace Bebruber.Domain.Models;

public class RideContext
{
    public RideContext(
        Client client,
        Driver driver,
        Route route,
        Location origin,
        Location destination,
        IReadOnlyCollection<Location> intermediatePoints)
    {
        Client = client.ThrowIfNull();
        Driver = driver.ThrowIfNull();
        Route = route.ThrowIfNull();
        Origin = origin.ThrowIfNull();
        Destination = destination.ThrowIfNull();
        IntermediatePoints = intermediatePoints.ThrowIfNull().ToList();
    }

    public Client Client { get; }
    public Driver Driver { get; }
    public Route Route { get; }
    public Location Origin { get; }
    public Location Destination { get; }
    public IReadOnlyCollection<Location> IntermediatePoints { get; }
}