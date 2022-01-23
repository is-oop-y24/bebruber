using Bebruber.DataAccess.Seeding;
using Bebruber.Identity;
using Bebruber.Utility.Tools;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Bebruber.DataAccess;

public sealed class IdentityDatabaseContext : IdentityDbContext<IdentityUser>
{
    private readonly TypeLocator _typeLocator;

    public IdentityDatabaseContext(DbContextOptions<IdentityDatabaseContext> options, TypeLocator typeLocator)
        : base(options)
    {
        _typeLocator = typeLocator;
        Database.EnsureCreated();
    }
}