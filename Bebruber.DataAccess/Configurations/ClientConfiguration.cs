using Bebruber.Domain.Entities;
using Bebruber.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bebruber.DataAccess.Configurations;

public class ClientConfiguration : IEntityTypeConfiguration<Client>
{
    public void Configure(EntityTypeBuilder<Client> builder)
    {
        builder.OwnsOne(c => c.Name);
        builder.OwnsOne(nameof(Rating), c => c.Rating);
        builder.OwnsOne(c => c.PaymentAddress);
        builder.Navigation(c => c.PaymentInfos).HasField("_paymentInfos");
    }
}