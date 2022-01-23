using System.Collections.Generic;
using Bebruber.Domain.Entities.Exceptions;
using Bebruber.Domain.Tools;
using Bebruber.Domain.ValueObjects.User;
using Bebruber.Utility.Extensions;

namespace Bebruber.Domain.Entities;

public class Driver : Entity<Driver>
{
    private readonly List<Ride> _rides;

    public Driver(Name name, Rating rating, PaymentAddress paymentAddress, CardInfo cardInfo, Car car)
    {
        _rides = new List<Ride>();
        Name = name.ThrowIfNull();
        Rating = rating.ThrowIfNull();
        PaymentAddress = paymentAddress.ThrowIfNull();
        CardInfo = cardInfo.ThrowIfNull();
        Car = car;
    }

    private Driver() { }

    public Name Name { get; private init; }
    public Rating Rating { get; set; }
    public PaymentAddress PaymentAddress { get; set; }
    public CardInfo CardInfo { get; set; }
    public Car Car { get; set; }
    public IReadOnlyCollection<Ride> Rides => _rides.AsReadOnly();

    public void AddRide(Ride ride)
    {
        ride.ThrowIfNull();

        if (_rides.Contains(ride))
            throw new OwnedRideException<Driver>(this, ride);

        _rides.Add(ride);
    }

    public void RemoveRide(Ride ride)
    {
        ride.ThrowIfNull();

        if (!_rides.Remove(ride))
            throw new NonOwnedRideException<Driver>(this, ride);
    }

    public override string ToString()
    => $"[{Id}] {Name}";
}