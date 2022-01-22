using Bebruber.Domain.Entities;
using Bebruber.Domain.Models;
using Bebruber.Domain.Services;
using Bebruber.Domain.ValueObjects.Ride;

namespace Bebruber.Core.Services;

public class RideService : IRideService
{
    private readonly IPricingService _pricingService;
    private readonly ITimeProviderService _timeProviderService;
    private readonly IClientNotificationService _clientNotificationService;

    public RideService(
        IPricingService pricingService,
        ITimeProviderService timeProviderService,
        IClientNotificationService clientNotificationService)
    {
        _pricingService = pricingService;
        _timeProviderService = timeProviderService;
        _clientNotificationService = clientNotificationService;
    }

    public Task<Ride> RegisterRideAsync(RideContext context, CancellationToken cancellationToken)
    {
        Roubles cost = _pricingService.Calculate(context);
        DateTime currentDateTime = _timeProviderService.GetCurrentDateTime();
        var ride = new Ride(
            context.Client,
            context.Driver,
            cost,
            currentDateTime,
            context.Origin,
            context.Destination,
            context.IntermediatePoints);

        return Task.FromResult(ride);
    }

    public async Task SetRideDriverArrivedAsync(Ride ride, CancellationToken cancellationToken)
    {
        await _clientNotificationService.NotifyDriverArrivedAsync(ride.Client, cancellationToken);
        ride.State = RideState.DriverArrived;
    }

    public async Task StartRideAsync(Ride ride, CancellationToken cancellationToken)
    {
        await _clientNotificationService.NotifyRideStartedAsync(ride.Client, cancellationToken);
        ride.State = RideState.Started;
    }

    public async Task FinishRideAsync(Ride ride, CancellationToken cancellationToken)
    {
        await _clientNotificationService.NotifyRideFinishedAsync(ride.Client, cancellationToken);
        ride.State = RideState.Finished;
    }
}