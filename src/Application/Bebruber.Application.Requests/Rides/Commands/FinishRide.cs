using MediatR;

namespace Bebruber.Application.Requests.Rides.Commands;

public static class FinishRide
{
    public record Command(Guid RideId) : IRequest<Response>;

    public record Response(bool Status);
}