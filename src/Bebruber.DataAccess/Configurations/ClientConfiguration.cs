using Bebruber.Domain.Entities;
using Bebruber.Domain.ValueObjects.User;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bebruber.DataAccess.Configurations;

internal class ClientConfiguration : EntityConfiguration<Client>
{
    protected override void ConfigureEntity(EntityTypeBuilder<Client> builder)
    {
        builder.OwnsOne(c => c.Name);
        builder.OwnsOne(nameof(Rating), c => c.Rating);
        builder.OwnsOne(c => c.PaymentAddress);
        builder.Navigation(c => c.PaymentInfos).HasField("_paymentInfos");
        builder.Navigation(c => c.Rides).HasField("_rides");
    }
}