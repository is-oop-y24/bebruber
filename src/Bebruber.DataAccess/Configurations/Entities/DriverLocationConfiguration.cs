using Bebruber.DataAccess.Configurations.ValueObjects;
using Bebruber.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bebruber.DataAccess.Configurations.Entities;

internal class DriverLocationConfiguration : EntityConfiguration<DriverLocation>
{
    protected override void ConfigureEntity(EntityTypeBuilder<DriverLocation> builder)
    {
        builder.OwnsOne(l => l.Coordinate).ConfigureShadowProperties();
    }
}