using MediatR;

namespace Bebruber.Application.Requests.Rides.Commands;

public static class StartRide
{
    public record Command(Guid RideId) : IRequest<Response>;

    public record Response(bool Status);
}