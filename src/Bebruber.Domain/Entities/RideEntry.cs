using System.Collections.Generic;
using System.Linq;
using Bebruber.Domain.Entities.Exceptions;
using Bebruber.Domain.Models;
using Bebruber.Domain.Tools;
using Bebruber.Domain.ValueObjects.Ride;
using Bebruber.Utility.Extensions;

namespace Bebruber.Domain.Entities;

public class RideEntry : Entity<RideEntry>
{
    private readonly List<Driver> _dismissedDrivers;

    public RideEntry(Location origin, Location destination, IReadOnlyCollection<Location> intermediatePoints, Client client)
    {
        Client = client;
        _dismissedDrivers = new List<Driver>();
        State = RideEntryState.Enqueued;
        Origin = origin.ThrowIfNull();
        Destination = destination.ThrowIfNull();
        IntermediatePoints = intermediatePoints.ThrowIfNull().ToList();
    }

    protected RideEntry() { }

    public virtual Location Origin { get; private init; }
    public virtual Location Destination { get; private init; }
    public virtual Client Client { get; private init; }
    public RideEntryState State { get; set; }
    public virtual IReadOnlyCollection<Location> IntermediatePoints { get; private init; }
    public virtual IReadOnlyCollection<Driver> DismissedDrivers => _dismissedDrivers.AsReadOnly();

    public void Dismiss(Driver driver)
    {
        if (_dismissedDrivers.Contains(driver))
            throw new AlreadyDismissedRideEntryException(this, driver);

        _dismissedDrivers.Add(driver);
    }

    public override string ToString()
        => $"{Id}";
}