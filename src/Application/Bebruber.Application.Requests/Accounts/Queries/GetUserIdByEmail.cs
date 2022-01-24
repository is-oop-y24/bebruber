using Bebruber.Domain.Entities;
using MediatR;

namespace Bebruber.Application.Requests.Accounts.Queries;

public static class GetUserIdByEmail
{
    public record Query(string Email) : IRequest<Response>;

    public record Response(Guid UserId);
}