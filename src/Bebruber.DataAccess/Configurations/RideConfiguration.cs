using Bebruber.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bebruber.DataAccess.Configurations;

public class RideConfiguration : IEntityTypeConfiguration<Ride>
{
    public void Configure(EntityTypeBuilder<Ride> builder)
    {
        builder.HasOne(r => r.Client);
        builder.HasOne(r => r.Driver);
        builder.OwnsOne(r => r.Cost);
        builder.OwnsOne(r => r.Origin);
        builder.OwnsOne(r => r.Destination);
        builder.OwnsMany(r => r.IntermediatePoints, p =>
        {
            p.WithOwner().HasForeignKey("OwnerId");
            p.Property<int>("Id");
            p.HasKey("Id");
        });
    }
}