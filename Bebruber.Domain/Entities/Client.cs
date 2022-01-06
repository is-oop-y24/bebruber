using System.Collections.Generic;
using Bebruber.Domain.Entities.Exceptions;
using Bebruber.Domain.Tools;
using Bebruber.Domain.ValueObjects;
using Bebruber.Utility.Extensions;

namespace Bebruber.Domain.Entities;

public class Client : Entity<Client>
{
    private readonly List<CardInfo> _paymentInfos;
    private readonly List<Ride> _rides;

    public Client(Name name, Rating rating, Address paymentAddress)
    {
        _paymentInfos = new List<CardInfo>();
        _rides = new List<Ride>();
        Name = name.ThrowIfNull();
        Rating = rating.ThrowIfNull();
        PaymentAddress = paymentAddress.ThrowIfNull();
    }

    private Client() { }

    public Name Name { get; protected init; }
    public Rating Rating { get; set; }
    public Address PaymentAddress { get; set; }
    public IReadOnlyCollection<CardInfo> PaymentInfos => _paymentInfos.AsReadOnly();
    public IReadOnlyCollection<Ride> Rides => _rides.AsReadOnly();

    public void AddPaymentInfo(CardInfo cardInfo)
    {
        cardInfo.ThrowIfNull();

        if (_paymentInfos.Contains(cardInfo))
            throw new OwnedCardInfoException(this, cardInfo);

        _paymentInfos.Add(cardInfo);
    }

    public void RemovePaymentInfo(CardInfo cardInfo)
    {
        cardInfo.ThrowIfNull();

        if (!_paymentInfos.Remove(cardInfo))
            throw new NonOwnedCardInfoException(this, cardInfo);
    }

    public void AddRide(Ride ride)
    {
        ride.ThrowIfNull();

        if (_rides.Contains(ride))
            throw new OwnedRideException<Client>(this, ride);

        _rides.Add(ride);
    }

    public void RemoveRide(Ride ride)
    {
        ride.ThrowIfNull();

        if (!_rides.Remove(ride))
            throw new NonOwnedRideException<Client>(this, ride);
    }
}