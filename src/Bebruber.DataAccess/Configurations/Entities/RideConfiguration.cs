using Bebruber.DataAccess.Configurations.ValueObjects;
using Bebruber.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bebruber.DataAccess.Configurations.Entities;

internal class RideConfiguration : EntityConfiguration<Ride>
{
    protected override void ConfigureEntity(EntityTypeBuilder<Ride> builder)
    {
        builder.HasOne(r => r.Client);
        builder.HasOne(r => r.Driver);
        builder.OwnsOne(r => r.Cost);
        builder.OwnsOne(r => r.Origin).ConfigureLocation();
        builder.OwnsOne(r => r.Destination).ConfigureLocation();
        builder.OwnsMany(r => r.IntermediatePoints, p =>
        {
            p.WithOwner().HasForeignKey("OwnerId");
            p.Property<int>("Id");
            p.HasKey("Id");
            p.ConfigureLocation();
        });
    }
}