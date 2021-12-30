using System.Collections.Generic;
using Bebruber.Domain.Entities.Exceptions;
using Bebruber.Domain.Tools;
using Bebruber.Domain.ValueObjects;
using Bebruber.Utility.Extensions;

namespace Bebruber.Domain.Entities;

public class Client : Entity<Client>
{
    private readonly List<CardInfo> _paymentInfos;

    public Client(Name name, Rating rating, Address paymentAddress)
    {
        _paymentInfos = new List<CardInfo>();
        Name = name.ThrowIfNull();
        Rating = rating.ThrowIfNull();
        PaymentAddress = paymentAddress.ThrowIfNull();
    }

    private Client() { }

    public Name Name { get; protected init; }
    public Rating Rating { get; set; }
    public Address PaymentAddress { get; set; }
    public IReadOnlyCollection<CardInfo> PaymentInfos => _paymentInfos.AsReadOnly();

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
}