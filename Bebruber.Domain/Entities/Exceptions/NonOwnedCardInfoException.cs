using Bebruber.Domain.Tools;

namespace Bebruber.Domain.Entities.Exceptions;

public class NonOwnedCardInfoException : BebruberException
{
    public NonOwnedCardInfoException(Client client, CardInfo cardInfo)
        : base($"{nameof(Client)} {client} does not own {nameof(CardInfo)} {cardInfo}") { }
}