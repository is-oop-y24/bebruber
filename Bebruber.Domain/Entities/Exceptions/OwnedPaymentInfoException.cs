using Bebruber.Domain.Tools;

namespace Bebruber.Domain.Entities.Exceptions;

public class OwnedPaymentInfoException : BebruberException
{
    public OwnedPaymentInfoException(Client client, PaymentInfo paymentInfo)
    : base($"{nameof(Client)} {client} already owns a {nameof(PaymentInfo)} {paymentInfo}") { }
}