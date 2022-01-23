using Bebruber.Domain.ValueObjects.Car;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bebruber.DataAccess.Configurations.ValueObjects;

public static class CarNumberConfiguration
{
    public static OwnedNavigationBuilder<TEntity, CarNumber> ConfigureCarNumber<TEntity>(
        this OwnedNavigationBuilder<TEntity, CarNumber> builder)
        where TEntity : class
    {
        builder.OwnsOne(l => l.RegionCode);
        builder.OwnsOne(l => l.RegistrationSeries);

        return builder;
    }
}