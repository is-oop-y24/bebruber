using System;
using System.Collections.Generic;
using System.Linq;
using Bebruber.Domain.Models;
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
        State = RideState.AwaitingDriver;
        Client = client.ThrowIfNull();
        Driver = driver.ThrowIfNull();
        Cost = cost.ThrowIfNull();
        DateTime = dateTime;
        Origin = origin.ThrowIfNull();
        Destination = destination.ThrowIfNull();
        IntermediatePoints = intermediatePoints.ThrowIfNull().ToList();
    }

    protected Ride() { }

    public RideState State { get; set; }
    public virtual Client Client { get; private init; }
    public virtual Driver Driver { get; private init; }
    public virtual Roubles Cost { get; private init; }
    public DateTime DateTime { get; private init; }
    public virtual Location Origin { get; private init; }
    public virtual Location Destination { get; private init; }
    public virtual IReadOnlyCollection<Location> IntermediatePoints { get; private init; }

    public override string ToString()
        => $"[{Id}] ClientId:{Client.Id} DriverId:{Driver.Id} {DateTime}";
}