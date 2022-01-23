using Bebruber.DataAccess.Configurations;
using Bebruber.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Bebruber.DataAccess;

public class DriverLocationDatabaseContext : DbContext
{
    public DriverLocationDatabaseContext(DbContextOptions<DriverLocationDatabaseContext> options)
        : base(options)
    {
        Database.EnsureCreated();
    }

    public DbSet<DriverLocation> Locations { get; private set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new DriverLocationConfiguration());
    }
}