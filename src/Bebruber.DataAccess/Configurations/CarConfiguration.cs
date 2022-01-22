using System.Drawing;
using Bebruber.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bebruber.DataAccess.Configurations;

internal class CarConfiguration : EntityConfiguration<Car>
{
    protected override void ConfigureEntity(EntityTypeBuilder<Car> builder)
    {
        builder.HasOne(c => c.Owner);
        builder.OwnsOne(d => d.CarNumber);
        builder.OwnsOne(c => c.Brand);
        builder.OwnsOne(c => c.Name);
        builder.OwnsOne(c => c.Color)
            .Property(c => c.Value)
            .HasConversion(c => c.ToArgb(),
                i => Color.FromArgb(i));
    }
}