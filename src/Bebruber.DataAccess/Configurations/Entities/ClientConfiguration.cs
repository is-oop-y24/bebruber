using Bebruber.DataAccess.Configurations.ValueObjects;
using Bebruber.Domain.Entities;
using Bebruber.Domain.ValueObjects.User;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bebruber.DataAccess.Configurations.Entities;

internal class ClientConfiguration : EntityConfiguration<Client>
{
    protected override void ConfigureEntity(EntityTypeBuilder<Client> builder)
    {
        builder.OwnsOne(c => c.Name).ConfigureShadowProperties();
        builder.OwnsOne(nameof(Rating), c => c.Rating).ConfigureShadowProperties();
        builder.OwnsOne(c => c.PhoneNumber).ConfigureShadowProperties();
        builder.Navigation(c => c.PaymentInfos).HasField("_paymentInfos");
        builder.Navigation(c => c.Rides).HasField("_rides");
    }
}