using Bebruber.Domain.Tools;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bebruber.DataAccess.Configurations;

internal class EntityConfiguration<T> : IEntityTypeConfiguration<T>
    where T : Entity<T>
{
    public void Configure(EntityTypeBuilder<T> builder)
    {
        builder.Property(e => e.Id).ValueGeneratedNever();
        ConfigureEntity(builder);
    }

    protected virtual void ConfigureEntity(EntityTypeBuilder<T> builder) { }
}