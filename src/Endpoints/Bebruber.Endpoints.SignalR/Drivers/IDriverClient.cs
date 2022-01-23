using Bebruber.Application.Requests.Rides.Commands;
using Bebruber.Common.Dto;

namespace Bebruber.Endpoints.SignalR.Drivers;

public interface IDriverClient
{
    Task OfferRideToDriverAsync(RideEntryDto rideEntry, TimeSpan awaitingTimeSpan);
    Task NotifySuccessfulAcceptanceAsync(RideDto rideEntry);
    Task NotifyFailedAcceptanceAsync();

    Task HandleUpdateLocationResponseAsync(UpdateDriverLocation.Response response);
}