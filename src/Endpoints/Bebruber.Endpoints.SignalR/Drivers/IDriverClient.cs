using Bebruber.Application.Requests.Rides.Commands;
using Bebruber.Common.Dto;

namespace Bebruber.Endpoints.SignalR.Drivers;

public interface IDriverClient
{
    Task OfferRideToDriver(RideEntryDto rideEntry, TimeSpan awaitingTimeSpan);
    Task NotifySuccessfulAcceptance(RideDto rideEntry);
    Task NotifyFailedAcceptance();

    Task HandleUpdateLocationResponse(UpdateDriverLocation.Response response);
}