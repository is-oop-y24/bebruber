using MediatR;

namespace Bebruber.Application.Requests.Drivers.Queries;

public static class GetDriverUserId
{
    public record Query(Guid DriverId) : IRequest<Response>;

    public record Response(string UserId);
}