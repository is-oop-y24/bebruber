using System.Threading.Tasks;
using Bebruber.Domain.Entities;
using Bebruber.Domain.Models;

namespace Bebruber.Domain.Services;

public interface IDriverNotificationService
{
    Task OfferRideToDriverAsync(Driver driver, RideEntry rideEntry);
    Task NotifySuccessfulAcceptanceAsync(Driver driver, Ride rideEntry);
    Task NotifyFailedAcceptanceAsync(Driver driver);
}