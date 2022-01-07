using Bebruber.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Bebruber.DataAccess;

public class BebruberDatabaseContext : DbContext
{
    public BebruberDatabaseContext(DbContextOptions options)
        : base(options) { }

    public DbSet<Client> Clients { get; private set; } = null!;
    public DbSet<CardInfo> PaymentInfos { get; private set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(IAssemblyMarker).Assembly);
    }
}