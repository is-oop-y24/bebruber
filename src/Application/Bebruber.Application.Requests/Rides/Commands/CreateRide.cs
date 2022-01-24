using Bebruber.Common.Dto;
using MediatR;

namespace Bebruber.Application.Requests.Rides.Commands;

public static class CreateRide
{
    public record ExternalCommand(
        LocationDto Origin,
        LocationDto Destination,
        string TaxiCategory,
        string PaymentMethod,
        IReadOnlyCollection<LocationDto> IntermediatePoints)
    { }

    public record Command(
        string Email,
        LocationDto Origin,
        LocationDto Destination,
        string TaxiCategory,
        string PaymentMethod,
        IReadOnlyCollection<LocationDto> IntermediatePoints) : IRequest<Response>;

    public record Response(Guid RideEntryId);
}