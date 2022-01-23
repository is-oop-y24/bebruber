using MediatR;

namespace Bebruber.Application.Requests.Rides.Commands;

public static class AcceptRide
{
    public record Command(Guid RideEntryId, Guid ClientId, Guid DriverId) : IRequest<Response>;

    public record Response(Guid RideId);
}