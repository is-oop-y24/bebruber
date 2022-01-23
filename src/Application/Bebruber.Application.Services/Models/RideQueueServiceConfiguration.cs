namespace Bebruber.Application.Services.Models;

public class RideQueueServiceConfiguration
{
    public RideQueueServiceConfiguration(TimeSpan awaitingTimeSpan)
    {
        AwaitingTimeSpan = awaitingTimeSpan;
    }

    public TimeSpan AwaitingTimeSpan { get; }
}