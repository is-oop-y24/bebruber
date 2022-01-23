using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace Bebruber.Identity.Tools;

public class JwtTokenGenerator : IJwtTokenGenerator
{
    private readonly SymmetricSecurityKey _key;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SigningConfigurations _signingConfigurations;
    private readonly JwtTokenOptions _tokenOptions;

    public JwtTokenGenerator(IConfiguration config, UserManager<IdentityUser> userManager, SigningConfigurations signingConfigurations, JwtTokenOptions tokenOptions)
    {
        _userManager = userManager;
        _signingConfigurations = signingConfigurations;
        _tokenOptions = tokenOptions;
        _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"]));
    }

    public string CreateToken(IdentityUser user)
    {
        var claims = new List<Claim> { new Claim(JwtRegisteredClaimNames.NameId, user.UserName) };

        var credentials = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

        var securityToken = new JwtSecurityToken(
            _tokenOptions.Issuer,
            _tokenOptions.Audience,
            GetClaims(user).Result,
            DateTime.UtcNow,
             DateTime.Now.AddDays(2),
            _signingConfigurations.SigningCredentials
        );
        var tokenHandler = new JwtSecurityTokenHandler();

        return tokenHandler.WriteToken(securityToken);
    }

    private async Task<IEnumerable<Claim>> GetClaims(IdentityUser user)
    {
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Sub, user.Email)
        };

        var roles = await _userManager.GetRolesAsync(user);
        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        return claims;
    }
}