using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bebruber.DataAccess.Configurations.ValueObjects;

public static class ValueObjectConfiguration
{
    public static OwnedNavigationBuilder<TEntity, TOwnedEntity> ConfigureShadowProperties<TEntity, TOwnedEntity>(
        this OwnedNavigationBuilder<TEntity, TOwnedEntity> builder)
        where TEntity : class
        where TOwnedEntity : class
    {
        builder.WithOwner().HasForeignKey("OwnerId");
        builder.Property<int>("Id");
        builder.HasKey("Id");

        return builder;
    }
}