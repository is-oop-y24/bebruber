using System.Threading.Tasks;
using Bebruber.Domain.Entities;
using Bebruber.Domain.Models;

namespace Bebruber.Domain.Services;

public interface IDriverNotificationService
{
    Task OfferRideToDriver(Driver driver, RideEntry rideEntry);
    Task NotifySuccessfulAcceptance(Driver driver, RideEntry rideEntry);
    Task NotifyFailedAcceptance(Driver driver);
}