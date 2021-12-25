using Bebruber.Domain.Tools;

namespace Bebruber.Domain.Entities.Exceptions;

public class NonOwnedPaymentInfoException : BebruberException
{
    public NonOwnedPaymentInfoException(Client client, PaymentInfo paymentInfo)
        : base($"{nameof(Client)} {client} does not own {nameof(PaymentInfo)} {paymentInfo}") { }
}