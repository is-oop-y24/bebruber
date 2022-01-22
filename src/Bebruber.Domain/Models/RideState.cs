namespace Bebruber.Domain.Models;

public enum RideState
{
    AwaitingDriver = 1,
    DriverArrived,
    Started,
    Finished,
}