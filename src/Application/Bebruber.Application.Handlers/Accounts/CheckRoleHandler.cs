using System.Security.Claims;
using Bebruber.Application.Requests.Accounts;
using MediatR;

namespace Bebruber.Application.Handlers.Accounts;

public class CheckRoleHandler : IRequestHandler<CheckRole.Command, CheckRole.Response>
{
    public Task<CheckRole.Response> Handle(CheckRole.Command request, CancellationToken cancellationToken)
    {
        IEnumerable<Claim> claims = request.Identity.Claims;
        var roles = claims.Where(c => c.Type == ClaimTypes.Role).ToList();
        return Task.FromResult(new CheckRole.Response(roles.Select(x => x.Value).Any(x => x.Equals(request.RoleName))));
    }
}