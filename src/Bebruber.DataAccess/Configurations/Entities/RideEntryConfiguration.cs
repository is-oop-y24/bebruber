using Bebruber.DataAccess.Configurations.ValueObjects;
using Bebruber.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bebruber.DataAccess.Configurations.Entities;

internal class RideEntryConfiguration : EntityConfiguration<RideEntry>
{
    protected override void ConfigureEntity(EntityTypeBuilder<RideEntry> builder)
    {
        builder.OwnsOne(e => e.Origin).ConfigureLocation();
        builder.OwnsOne(e => e.Destination).ConfigureLocation();
        builder.OwnsMany(r => r.IntermediatePoints, p =>
        {
            p.ConfigureShadowProperties();
            p.ConfigureLocation();
        });
    }
}