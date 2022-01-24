using Bebruber.DataAccess.Configurations.ValueObjects;
using Bebruber.Domain.Entities;
using Bebruber.Domain.ValueObjects.Card;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bebruber.DataAccess.Configurations.Entities;

internal class CardInfoConfiguration : EntityConfiguration<CardInfo>
{
    protected override void ConfigureEntity(EntityTypeBuilder<CardInfo> builder)
    {
        builder.OwnsOne(nameof(CardNumber), i => i.CardNumber);
        builder.OwnsOne(nameof(CvvCode), i => i.CvvCode);
        builder.OwnsOne(i => i.ExpirationDate);
    }
}