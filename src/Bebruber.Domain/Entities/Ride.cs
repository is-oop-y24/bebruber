using System;
using System.Collections.Generic;
using System.Linq;
using Bebruber.Domain.Tools;
using Bebruber.Domain.ValueObjects.Ride;
using Bebruber.Utility.Extensions;

namespace Bebruber.Domain.Entities;

public class Ride : Entity<Ride>
{
    public Ride(
        Client client,
        Driver driver,
        Roubles cost,
        DateTime dateTime,
        Location origin,
        Location destination,
        IReadOnlyCollection<Location> intermediatePoints)
    {
        Client = client.ThrowIfNull();
        Driver = driver.ThrowIfNull();
        Cost = cost.ThrowIfNull();
        DateTime = dateTime;
        Origin = origin.ThrowIfNull();
        Destination = destination.ThrowIfNull();
        IntermediatePoints = intermediatePoints.ThrowIfNull().ToList();
    }

    public Client Client { get; private init; }
    public Driver Driver { get; private init; }
    public Roubles Cost { get; private init; }
    public DateTime DateTime { get; private init; }
    public Location Origin { get; private init; }
    public Location Destination { get; private init; }
    public IReadOnlyCollection<Location> IntermediatePoints { get; private init; }
}