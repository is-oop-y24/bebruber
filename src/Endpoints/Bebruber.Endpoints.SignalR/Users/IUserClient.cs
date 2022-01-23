using Bebruber.Common.Dto;

namespace Bebruber.Endpoints.SignalR.Users;

public interface IUserClient
{
    Task PostDriverCoordinates(CoordinateDto coordinate);
    Task NotifyDriverFound();
    Task NotifyDriverArrived();
    Task NotifyRideStarted();
    Task NotifyRideFinished();
}