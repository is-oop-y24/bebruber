using System.Collections.Generic;
using Bebruber.Domain.Entities.Exceptions;
using Bebruber.Domain.Tools;
using Bebruber.Domain.ValueObjects;
using Bebruber.Utility.Extensions;

namespace Bebruber.Domain.Entities;

public class Driver : Entity<Driver>
{
    private readonly List<Car> _cars;
    private readonly List<Ride> _rides;

    public Driver(Name name, Rating rating, Address paymentAddress, CardInfo cardInfo)
    {
        _cars = new List<Car>();
        _rides = new List<Ride>();
        Name = name.ThrowIfNull();
        Rating = rating.ThrowIfNull();
        PaymentAddress = paymentAddress.ThrowIfNull();
        CardInfo = cardInfo.ThrowIfNull();
    }

    private Driver() { }

    public Name Name { get; private init; }
    public Rating Rating { get; set; }
    public Address PaymentAddress { get; set; }
    public CardInfo CardInfo { get; set; }
    public IReadOnlyCollection<Car> Cars => _cars.AsReadOnly();
    public IReadOnlyCollection<Ride> Rides => _rides.AsReadOnly();

    public void AddCar(Car car)
    {
        car.ThrowIfNull();

        if (_cars.Contains(car))
            throw new OwnedCarException(this, car);

        _cars.Add(car);
    }

    public void RemoveCar(Car car)
    {
        car.ThrowIfNull();

        if (!_cars.Remove(car))
            throw new NonOwnedCarException(this, car);
    }

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
}