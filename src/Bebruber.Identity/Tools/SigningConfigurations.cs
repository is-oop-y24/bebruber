using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Bebruber.Identity.Tools;

public class SigningConfigurations
{
    public SecurityKey SecurityKey { get; }
    public SigningCredentials SigningCredentials { get; }

    public SigningConfigurations(string key)
    {
        byte[] keyBytes = Encoding.ASCII.GetBytes(key);

        SecurityKey = new SymmetricSecurityKey(keyBytes);
        SigningCredentials = new SigningCredentials(SecurityKey, SecurityAlgorithms.HmacSha256Signature);
    }
}