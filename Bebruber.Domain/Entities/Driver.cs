using System.Collections.Generic;
using Bebruber.Domain.Entities.Exceptions;
using Bebruber.Domain.Tools;
using Bebruber.Domain.ValueObjects;
using Bebruber.Utility.Extensions;

namespace Bebruber.Domain.Entities;

public class Driver : Entity<Driver>
{
    private readonly List<Car> _cars;

    public Driver(Name name, Rating rating, Address paymentAddress, CarNumber number, CardInfo cardInfo)
    {
        _cars = new List<Car>();
        Name = name.ThrowIfNull();
        Rating = rating.ThrowIfNull();
        PaymentAddress = paymentAddress.ThrowIfNull();
        CarNumber = number.ThrowIfNull();
        CardInfo = cardInfo.ThrowIfNull();
    }

    private Driver() { }

    public Name Name { get; private init; }
    public Rating Rating { get; set; }
    public Address PaymentAddress { get; set; }
    public CarNumber CarNumber { get; set; }
    public CardInfo CardInfo { get; set; }
    public IReadOnlyCollection<Car> Cars => _cars.AsReadOnly();

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
}