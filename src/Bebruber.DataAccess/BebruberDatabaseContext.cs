using Bebruber.DataAccess.Seeding;
using Bebruber.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Bebruber.DataAccess;

public sealed class BebruberDatabaseContext : DbContext
{
    private readonly BebruberDatabaseSeeder _seeder;

    public BebruberDatabaseContext(DbContextOptions<BebruberDatabaseContext> options, BebruberDatabaseSeeder seeder)
        : base(options)
    {
        _seeder = seeder;
        Database.EnsureCreated();
    }

    public DbSet<Client> Clients { get; private set; } = null!;
    public DbSet<CardInfo> PaymentInfos { get; private set; } = null!;
    public DbSet<Ride> Rides { get; private set; } = null!;
    public DbSet<Driver> Drivers { get; private set; } = null!;
    public DbSet<Car> Cars { get; private set; } = null!;
    public DbSet<DriverLocation> Locations { get; private set; } = null!;
    public DbSet<RideEntry> Entries { get; private set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(IAssemblyMarker).Assembly);
        _seeder.Seed(modelBuilder);
    }
}