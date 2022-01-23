using Microsoft.AspNetCore.Identity;

namespace Bebruber.Identity;

public class ApplicationUser : IdentityUser
{
    public ApplicationUser() : base()
    { }

    public string ModelType { get; init; } = String.Empty;
    public Guid ModelId { get; init; } = Guid.Empty;
}