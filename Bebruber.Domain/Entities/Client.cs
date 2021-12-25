using System.Collections.Generic;
using Bebruber.Domain.Entities.Exceptions;
using Bebruber.Domain.Tools;
using Bebruber.Domain.ValueObjects;
using Bebruber.Utility.Extensions;

namespace Bebruber.Domain.Entities;

public class Client : Entity<Client>
{
    private readonly List<PaymentInfo> _paymentInfos;

    public Client(Name name, Rating rating, Address? paymentAddress)
    {
        Name = name.ThrowIfNull();
        Rating = rating.ThrowIfNull();
        PaymentAddress = paymentAddress;
        _paymentInfos = new List<PaymentInfo>();
    }

#pragma warning disable CS8618
    private Client() { }
#pragma warning restore CS8618

    public Name Name { get; private init; }
    public Rating Rating { get; set; }
    public Address? PaymentAddress { get; private set; }
    public IReadOnlyCollection<PaymentInfo> PaymentInfos => _paymentInfos;

    public void AddPaymentInfo(PaymentInfo paymentInfo)
    {
        if (_paymentInfos.Contains(paymentInfo))
            throw new OwnedPaymentInfoException(this, paymentInfo);

        _paymentInfos.Add(paymentInfo);
    }

    public void RemovePaymentInfo(PaymentInfo paymentInfo)
    {
        if (!_paymentInfos.Remove(paymentInfo))
            throw new NonOwnedPaymentInfoException(this, paymentInfo);
    }
}