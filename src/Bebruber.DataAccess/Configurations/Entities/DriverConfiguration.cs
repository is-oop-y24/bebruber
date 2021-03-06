using Bebruber.DataAccess.Configurations.ValueObjects;
using Bebruber.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bebruber.DataAccess.Configurations.Entities;

internal class DriverConfiguration : EntityConfiguration<Driver>
{
    protected override void ConfigureEntity(EntityTypeBuilder<Driver> builder)
    {
        builder.OwnsOne(d => d.Name);
        builder.OwnsOne(d => d.Rating);
        builder.OwnsOne(d => d.PhoneNumber);
        builder.OwnsOne(d => d.Email);
        builder.HasOne(d => d.CardInfo);
        builder.HasOne(d => d.Car);
        builder.Navigation(d => d.Rides).HasField("_rides");
    }
}