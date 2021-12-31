using System.Drawing;
using Bebruber.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bebruber.DataAccess.Configurations;

public class CarConfiguration : IEntityTypeConfiguration<Car>
{
    public void Configure(EntityTypeBuilder<Car> builder)
    {
        builder.HasOne(c => c.Owner);
        builder.OwnsOne(d => d.CarNumber);
        builder.OwnsOne(c => c.Brand);
        builder.OwnsOne(c => c.Name);
        builder.OwnsOne(c => c.Color)
            .Property(c => c.Color)
            .HasConversion(c => c.ToArgb(),
                i => Color.FromArgb(i));
    }
}