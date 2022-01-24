using Bebruber.Domain.Tools;
using Bebruber.Domain.ValueObjects.Card;
using Bebruber.Utility.Extensions;

namespace Bebruber.Domain.Entities;

public class CardInfo : Entity<CardInfo>
{
    public CardInfo(CardNumber cardNumber, ExpirationDate expirationDate, CvvCode cvvCode)
    {
        CardNumber = cardNumber.ThrowIfNull();
        ExpirationDate = expirationDate.ThrowIfNull();
        CvvCode = cvvCode.ThrowIfNull();
    }

    protected CardInfo() { }

    public virtual CardNumber CardNumber { get; private init; }
    public virtual ExpirationDate ExpirationDate { get; private init; }
    public virtual CvvCode CvvCode { get; private init; }

    public override string ToString()
        => $"[{Id}] {CardNumber}";
}