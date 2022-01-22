using Bebruber.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bebruber.DataAccess.Configurations;

internal class RideEntryConfiguration : EntityConfiguration<RideEntry>
{
    protected override void ConfigureEntity(EntityTypeBuilder<RideEntry> builder)
    {
        builder.OwnsOne(e => e.Origin);
        builder.OwnsOne(e => e.Destination);
        builder.Navigation(e => e.DismissedDrives).HasField("_dismissedDrivers");
        builder.OwnsMany(r => r.IntermediatePoints, p =>
        {
            p.WithOwner().HasForeignKey("OwnerId");
            p.Property<int>("Id");
            p.HasKey("Id");
        });
    }
}