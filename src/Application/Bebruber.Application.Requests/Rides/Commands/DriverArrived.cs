using MediatR;

namespace Bebruber.Application.Requests.Rides.Commands;

public static class DriverArrived
{
    public record Command(Guid RideId, Guid ClientId) : IRequest<Response>;

    public record Response(bool Status);
}