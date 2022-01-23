using Bebruber.Identity;
using Bebruber.Utility.Tools;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Bebruber.DataAccess;

public sealed class IdentityDatabaseContext : IdentityDbContext<ApplicationUser>
{
    private readonly TypeLocator _typeLocator;

    public IdentityDatabaseContext(DbContextOptions<IdentityDatabaseContext> options, TypeLocator typeLocator)
        : base(options)
    {
        _typeLocator = typeLocator;
        Database.EnsureCreated();
    }

    public override void Dispose()
    {
        Database.EnsureDeleted();
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<ApplicationUser>()
               .Property(u => u.ModelType)
               .HasConversion(
                   t => _typeLocator.GetKey(t),
                   s => _typeLocator.Resolve(s));
        base.OnModelCreating(builder);
    }
}