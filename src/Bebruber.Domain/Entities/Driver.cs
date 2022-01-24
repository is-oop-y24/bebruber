using System.Collections.Generic;
using Bebruber.Domain.Entities.Exceptions;
using Bebruber.Domain.Tools;
using Bebruber.Domain.ValueObjects.User;
using Bebruber.Utility.Extensions;

namespace Bebruber.Domain.Entities;

public class Driver : Entity<Driver>
{
    private readonly List<Ride> _rides;

    public Driver(
        Name name,
        Rating rating,
        CardInfo cardInfo,
        Car car,
        PhoneNumber phoneNumber,
        Email email)
    {
        _rides = new List<Ride>();
        Name = name.ThrowIfNull();
        Rating = rating.ThrowIfNull();
        CardInfo = cardInfo.ThrowIfNull();
        Car = car.ThrowIfNull();
        PhoneNumber = phoneNumber.ThrowIfNull();
        Email = email.ThrowIfNull();
    }

    protected Driver() { }

    public virtual Name Name { get; private init; }
    public virtual Rating Rating { get; set; }
    public virtual PhoneNumber PhoneNumber { get; set; }
    public virtual Email Email { get; set; }
    public virtual CardInfo CardInfo { get; set; }
    public virtual Car Car { get; set; }
    public virtual IReadOnlyCollection<Ride> Rides => _rides.AsReadOnly();

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