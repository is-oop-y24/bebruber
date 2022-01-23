using Microsoft.AspNetCore.Identity;

namespace Bebruber.Identity.Tools;

public interface IJwtTokenGenerator
{
    string CreateToken(ApplicationUser user);
}