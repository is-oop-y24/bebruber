using System;

namespace Bebruber.Application.Models;

public class RideQueueServiceConfiguration
{
    public RideQueueServiceConfiguration(TimeSpan awaitingTimeSpan)
    {
        AwaitingTimeSpan = awaitingTimeSpan;
    }

    public TimeSpan AwaitingTimeSpan { get; }
}