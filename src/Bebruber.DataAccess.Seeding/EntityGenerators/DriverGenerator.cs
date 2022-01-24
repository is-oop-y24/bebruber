using Bebruber.Domain.Entities;
using Bebruber.Domain.ValueObjects.Card;
using Bebruber.Domain.ValueObjects.User;
using Bebruber.Identity;
using Microsoft.AspNetCore.Identity;

namespace Bebruber.DataAccess.Seeding.EntityGenerators;

public class DriverGenerator : IEntityGenerator
{
    public DriverGenerator(
        UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, CarGenerator carGenerator)
    {
        Drivers = CreateDrivers(userManager, roleManager, carGenerator);
    }

    public IReadOnlyList<Driver> Drivers { get; }

    public void Seed(BebruberDatabaseContext context)
    {
        context.Drivers.AddRange(Drivers);
    }

    private static IReadOnlyList<Driver> CreateDrivers(
        UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, CarGenerator carGenerator)
    {
        var firstDriver = new Driver(
            new Name("Bebra", "Vodila", "Mudila"),
            new Rating(10),
            new CardInfo(new CardNumber("0000000000000000"), new ExpirationDate(2023, 10), new CvvCode("123")),
            carGenerator.Cars[0],
            new PhoneNumber("88005553535"),
            new Email("mud@bebra.com"));

        var secondDriver = new Driver(
            new Name("Bebra", "Vodila", "Sestra-Rodila"),
            new Rating(10),
            new CardInfo(new CardNumber("0000000000000000"), new ExpirationDate(2023, 10), new CvvCode("123")),
            carGenerator.Cars[1],
            new PhoneNumber("88005553535"),
            new Email("ses@bebra.com"));

        var thirdDriver = new Driver(
            new Name("Bebra", "Vodila", "Rossia-Pobedila"),
            new Rating(10),
            new CardInfo(new CardNumber("0000000000000000"), new ExpirationDate(2023, 10), new CvvCode("123")),
            carGenerator.Cars[2],
            new PhoneNumber("88005553535"),
            new Email("Ros@bebra.com"));

        var firstUser = new ApplicationUser
        {
            Email = firstDriver.Email.Value,
            UserName = "MudBebra",
            ModelId = firstDriver.Id,
            ModelType = typeof(Driver),
        };

        var secondUser = new ApplicationUser
        {
            Email = secondDriver.Email.Value,
            UserName = "SesBebra",
            ModelId = secondDriver.Id,
            ModelType = typeof(Driver),
        };

        var thirdUser = new ApplicationUser
        {
            Email = thirdDriver.Email.Value,
            UserName = "RosBebra",
            ModelId = thirdDriver.Id,
            ModelType = typeof(Driver),
        };

        var driverRole = new IdentityRole { Name = "Driver" };

        userManager.CreateAsync(firstUser).GetAwaiter().GetResult();
        userManager.CreateAsync(secondUser).GetAwaiter().GetResult();
        userManager.CreateAsync(thirdUser).GetAwaiter().GetResult();

        roleManager.CreateAsync(driverRole).GetAwaiter().GetResult();

        userManager.AddToRoleAsync(firstUser, driverRole.Name).GetAwaiter().GetResult();
        userManager.AddToRoleAsync(secondUser, driverRole.Name).GetAwaiter().GetResult();
        userManager.AddToRoleAsync(thirdUser, driverRole.Name).GetAwaiter().GetResult();

        return new[] { firstDriver, secondDriver, thirdDriver };
    }
}