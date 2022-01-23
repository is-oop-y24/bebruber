using Bebruber.Domain.Services;

namespace Bebruber.Application.Services;

public class TimeProviderService : ITimeProviderService
{
    public DateTime GetCurrentDateTime()
        => DateTime.UtcNow;
}