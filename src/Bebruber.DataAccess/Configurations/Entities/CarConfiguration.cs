using System.Drawing;
using Bebruber.DataAccess.Configurations.ValueObjects;
using Bebruber.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bebruber.DataAccess.Configurations.Entities;

internal class CarConfiguration : EntityConfiguration<Car>
{
    protected override void ConfigureEntity(EntityTypeBuilder<Car> builder)
    {
        builder.OwnsOne(d => d.Number).ConfigureCarNumber();
        builder.OwnsOne(c => c.Brand);
        builder.OwnsOne(c => c.Name);
        builder.OwnsOne(c => c.Color)
            .Property(c => c.Value)
            .HasConversion(
                c => c.ToArgb(),
                i => Color.FromArgb(i));
        builder.OwnsOne(c => c.Category);
    }
}