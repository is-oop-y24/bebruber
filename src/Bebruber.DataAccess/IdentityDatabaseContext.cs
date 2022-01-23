using Bebruber.DataAccess.Seeding;
using Bebruber.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Bebruber.DataAccess;

public class IdentityDatabaseContext : IdentityDbContext<ApplicationUser>
{
    public IdentityDatabaseContext(DbContextOptions<IdentityDatabaseContext> options)
        : base(options)
    {
        Database.EnsureCreated();
    }
}