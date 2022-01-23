using Bebruber.DataAccess.Configurations;
using Bebruber.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Bebruber.DataAccess;

public class RideEntryDatabaseContext : DbContext
{
    public RideEntryDatabaseContext(DbContextOptions<RideEntryDatabaseContext> options)
        : base(options) { }

    public DbSet<RideEntry> Entries { get; private set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new RideEntryConfiguration());
    }
}