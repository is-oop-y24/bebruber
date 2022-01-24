using System.Security.Claims;

namespace Bebruber.Utility.Extensions;

public static class ClaimsExtensions
{
    public static string GetEmail(this IEnumerable<Claim> claims)
    {
        Claim? claim = claims.FirstOrDefault(c => c.Type.Equals(ClaimTypes.NameIdentifier));

        // TODO: fix
        string email = claim?.Value ?? throw new Exception("Email not found");
        return email;
    }
}