using Bebruber.Domain.Entities;
using Bebruber.Domain.ValueObjects.Card;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bebruber.DataAccess.Configurations;

internal class CardInfoConfiguration : IEntityTypeConfiguration<CardInfo>
{
    public void Configure(EntityTypeBuilder<CardInfo> builder)
    {
        builder.OwnsOne(nameof(CardNumber), i => i.CardNumber);
        builder.OwnsOne(nameof(CvvCode), i => i.CvvCode);
        builder.OwnsOne(i => i.ExpirationDate);
    }
}