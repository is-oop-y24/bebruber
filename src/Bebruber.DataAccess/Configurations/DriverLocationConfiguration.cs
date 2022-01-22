using Bebruber.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bebruber.DataAccess.Configurations;

internal class DriverLocationConfiguration : EntityConfiguration<DriverLocation>
{
    protected override void ConfigureEntity(EntityTypeBuilder<DriverLocation> builder)
    {
        builder.HasOne(l => l.Driver);
        builder.OwnsOne(l => l.Coordinate);
        builder.Property(l => l.LastUpdateTime);
    }
}