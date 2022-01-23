using Bebruber.Domain.Entities;
using Bebruber.Domain.Services;
using Bebruber.Domain.ValueObjects.Ride;

namespace Bebruber.Application.Services;

public class PaymentService : IPaymentService
{
    public Task WithdrawAsync(CardInfo cardInfo, Roubles amount, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task AccrueAsync(CardInfo cardInfo, Roubles amount, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}