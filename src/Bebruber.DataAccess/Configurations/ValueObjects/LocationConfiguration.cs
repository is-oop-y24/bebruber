using Bebruber.Domain.ValueObjects.Ride;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bebruber.DataAccess.Configurations.ValueObjects;

public static class LocationConfiguration
{
    public static OwnedNavigationBuilder<TEntity, Location> ConfigureLocation<TEntity>(
        this OwnedNavigationBuilder<TEntity, Location> builder)
        where TEntity : class
    {
        builder.OwnsOne(l => l.Address);
        builder.OwnsOne(l => l.Coordinate);
        builder.ConfigureShadowProperties();

        return builder;
    }
}