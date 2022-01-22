using System;
using System.Threading;
using System.Threading.Tasks;
using Bebruber.DataAccess;
using Bebruber.Domain.Entities;
using Bebruber.Domain.Models;
using Bebruber.Domain.Services;
using Bebruber.Domain.ValueObjects.Ride;
using Bebruber.Utility.Extensions;

namespace Bebruber.Application.Services;

public class RideService : IRideService
{
    private readonly BebruberDatabaseContext _context;
    private readonly IPricingService _pricingService;
    private readonly ITimeProviderService _timeProviderService;
    private readonly IClientNotificationService _clientNotificationService;

    public RideService(
        BebruberDatabaseContext context,
        IPricingService pricingService,
        ITimeProviderService timeProviderService,
        IClientNotificationService clientNotificationService)
    {
        _pricingService = pricingService;
        _timeProviderService = timeProviderService;
        _clientNotificationService = clientNotificationService;
        _context = context.ThrowIfNull();
    }

    public async Task<Ride> RegisterRideAsync(RideContext context, CancellationToken cancellationToken)
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

        context.Client.AddRide(ride);
        context.Driver.AddRide(ride);

        _context.Clients.Update(context.Client);
        _context.Drivers.Update(context.Driver);
        await _context.Rides.AddAsync(ride, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return ride;
    }

    public async Task SetRideDriverArrivedAsync(Ride ride, CancellationToken cancellationToken)
    {
        await _clientNotificationService.NotifyDriverArrivedAsync(ride.Client, cancellationToken);
        ride.State = RideState.DriverArrived;

        _context.Rides.Update(ride);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task StartRideAsync(Ride ride, CancellationToken cancellationToken)
    {
        await _clientNotificationService.NotifyRideStartedAsync(ride.Client, cancellationToken);
        ride.State = RideState.Started;

        _context.Rides.Update(ride);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task FinishRideAsync(Ride ride, CancellationToken cancellationToken)
    {
        await _clientNotificationService.NotifyRideFinishedAsync(ride.Client, cancellationToken);
        ride.State = RideState.Finished;

        _context.Rides.Update(ride);
        await _context.SaveChangesAsync(cancellationToken);
    }
}