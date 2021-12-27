using Bebruber.Domain.Tools;

namespace Bebruber.Domain.Entities.Exceptions;

public class OwnedCardInfoException : BebruberException
{
    public OwnedCardInfoException(Client client, CardInfo cardInfo)
    : base($"{nameof(Client)} {client} already owns a {nameof(CardInfo)} {cardInfo}") { }
}