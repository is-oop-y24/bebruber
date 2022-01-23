using Bebruber.Common.Dto;
using MediatR;

namespace Bebruber.Application.Requests.Rides.Commands;

public static class DriverLocationUpdate
{
    public record Command(Guid DriverId, CoordinateDto Coord) : IRequest<Response>;

    public record Response(bool Status);
}