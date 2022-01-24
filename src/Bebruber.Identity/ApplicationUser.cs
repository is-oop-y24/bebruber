using Microsoft.AspNetCore.Identity;

namespace Bebruber.Identity;

public class ApplicationUser : IdentityUser
{
    public Type? ModelType { get; init; }
    public Guid ModelId { get; init; } = Guid.Empty;
}