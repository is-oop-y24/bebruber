using System.Security.Claims;
using MediatR;

namespace Bebruber.Application.Requests.Accounts;

public class CheckRole
{
    public record Command(ClaimsIdentity Identity, string RoleName) : IRequest<Response>;
    public record Response(bool IsValid);
}