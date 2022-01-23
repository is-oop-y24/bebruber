using Microsoft.AspNetCore.Identity;

namespace Bebruber.Identity;

public class ApplicationUser : IdentityUser
{
    public ApplicationUser() : base()
    { }

    public Type ModelType { get; init; }
    public Guid ModelId { get; init; } = Guid.Empty;
}