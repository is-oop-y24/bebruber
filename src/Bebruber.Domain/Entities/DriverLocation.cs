using System;
using System.Collections.Generic;
using Bebruber.Domain.Entities.Exceptions;
using Bebruber.Domain.Tools;
using Bebruber.Domain.ValueObjects.Ride;
using Bebruber.Utility.Extensions;

namespace Bebruber.Domain.Entities;

public class DriverLocation : Entity<DriverLocation>
{
    private readonly List<Client> _subscribers;

    public DriverLocation(Driver driver, Coordinate coordinate, DateTime lastUpdateTime)
    {
        _subscribers = new List<Client>();
        Driver = driver.ThrowIfNull();
        Coordinate = coordinate.ThrowIfNull();
        LastUpdateTime = lastUpdateTime;
    }

    protected DriverLocation() { }

    public virtual Driver Driver { get; private init; }
    public virtual Coordinate Coordinate { get; set; }
    public DateTime LastUpdateTime { get; set; }
    public IReadOnlyCollection<Client> Subscribers => _subscribers.AsReadOnly();

    public override string ToString()
        => $"[{Id}] {Driver} {Coordinate} {LastUpdateTime}";

    public void Subscribe(Client client)
    {
        if (_subscribers.Contains(client))
            throw new AlreadySubscribedClientException(client, Driver);

        _subscribers.Add(client);
    }

    public void Unsubscribe(Client client)
    {
        if (!_subscribers.Remove(client))
            throw new NotSubscribedClientException(client, Driver);
    }
}