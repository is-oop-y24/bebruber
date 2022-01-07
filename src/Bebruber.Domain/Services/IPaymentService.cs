using System.Threading;
using System.Threading.Tasks;
using Bebruber.Domain.Entities;
using Bebruber.Domain.ValueObjects.Ride;

namespace Bebruber.Domain.Services;

public interface IPaymentService
{
    Task WithdrawAsync(CardInfo cardInfo, Roubles amount, CancellationToken cancellationToken);
    Task AccrueAsync(CardInfo cardInfo, Roubles amount, CancellationToken cancellationToken);
}