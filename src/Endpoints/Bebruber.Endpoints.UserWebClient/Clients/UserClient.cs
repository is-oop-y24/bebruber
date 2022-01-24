using System;
using System.Threading.Tasks;
using Bebruber.Common.Dto;
using Bebruber.Endpoints.SignalR.Users;

namespace Bebruber.Endpoints.UserWebClient.Clients;

public class UserClient : IUserClient
{
    private Action<CoordinateDto> _driverCoordinatesUpdated;
    private Action _driverArrived;
    private Action _driverFound;
    private Action _rideStarted;
    private Action _rideFinished;

    public UserClient(
        Action<CoordinateDto> driverCoordinatesUpdated,
        Action driverFound,
        Action driverArrived,
        Action rideStarted,
        Action rideFinished)
    {
        _driverCoordinatesUpdated = driverCoordinatesUpdated;
        _driverFound = driverFound;
        _driverArrived = driverArrived;
        _rideStarted = rideStarted;
        _rideFinished = rideFinished;
    }

    public Task PostDriverCoordinates(CoordinateDto coordinate)
    {
        _driverCoordinatesUpdated.Invoke(coordinate);
        return Task.CompletedTask;
    }

    public Task NotifyDriverFound()
    {
        _driverFound.Invoke();
        return Task.CompletedTask;
    }

    public Task NotifyDriverArrived()
    {
        _driverArrived.Invoke();
        return Task.CompletedTask;
    }

    public Task NotifyRideStarted()
    {
        _rideStarted.Invoke();
        return Task.CompletedTask;
    }

    public Task NotifyRideFinished()
    {
        _rideFinished.Invoke();
        return Task.CompletedTask;
    }
}