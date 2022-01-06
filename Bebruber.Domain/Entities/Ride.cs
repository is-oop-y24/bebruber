using System;
using Bebruber.Domain.Tools;
using Bebruber.Domain.ValueObjects;

namespace Bebruber.Domain.Entities;

public class Ride : Entity<Ride>
{
    public Ride(Client client, Driver driver, Roubles cost, DateTime dateTime)
    {
        Client = client;
        Driver = driver;
        Cost = cost;
        DateTime = dateTime;
    }

    public Client Client { get; private init; }
    public Driver Driver { get; private init; }
    public Roubles Cost { get; private init; }
    public DateTime DateTime { get; private init; }
}