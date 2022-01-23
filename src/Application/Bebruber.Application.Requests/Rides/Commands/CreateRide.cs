using Bebruber.Common.Dto;
using MediatR;

namespace Bebruber.Application.Requests.Rides.Commands;

public static class CreateRide
{
    public record Command(
        LocationDto Origin,
        LocationDto Destination,
        IReadOnlyCollection<LocationDto> IntermediatePoints) : IRequest<Response>;

    public record Response(Guid RideEntryId);
}