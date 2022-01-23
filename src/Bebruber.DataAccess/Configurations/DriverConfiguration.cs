using Bebruber.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bebruber.DataAccess.Configurations;

internal class DriverConfiguration : EntityConfiguration<Driver>
{
    protected override void ConfigureEntity(EntityTypeBuilder<Driver> builder)
    {
        builder.OwnsOne(d => d.Name);
        builder.OwnsOne(d => d.Rating);
        builder.OwnsOne(d => d.PaymentAddress);
        builder.OwnsOne(d => d.PhoneNumber);
        builder.HasOne(d => d.CardInfo);
        builder.HasOne(d => d.Car);
        builder.Navigation(d => d.Rides).HasField("_rides");
    }
}