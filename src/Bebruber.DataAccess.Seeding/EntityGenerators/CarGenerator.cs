using Bebruber.Domain.Entities;
using Bebruber.Domain.Enumerations;
using Bebruber.Domain.ValueObjects.Car;
using Microsoft.EntityFrameworkCore;

namespace Bebruber.DataAccess.Seeding.EntityGenerators;

public class CarGenerator : IEntityGenerator
{
    public CarGenerator()
    {
        Cars = CreateCars();
    }

    public IReadOnlyList<Car> Cars { get; }

    public void Seed(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Car>().HasData(Cars);
    }

    private static IReadOnlyList<Car> CreateCars()
    {
        Car[] cars =
        {
            new Car(new CarBrand("Bently"), new CarName("Huently"), CarColor.Black, CarCategory.Business),
            new Car(new CarBrand("Lada"), new CarName("Calina"), CarColor.White, CarCategory.Economy),
            new Car(new CarBrand("Reno"), new CarName("Logan"), CarColor.Black, CarCategory.Comfort),
        };

        return cars;
    }
}