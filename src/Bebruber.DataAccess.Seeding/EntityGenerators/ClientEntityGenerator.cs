using Bebruber.Domain.Entities;
using Bebruber.Domain.ValueObjects.User;
using Bebruber.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Bebruber.DataAccess.Seeding.EntityGenerators;

public class ClientEntityGenerator : IEntityGenerator
{
    public ClientEntityGenerator(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        Clients = CreateClients(userManager, roleManager);
    }

    public IReadOnlyCollection<Client> Clients { get; }

    public void Seed(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Client>().HasData(Clients);
    }

    private static IReadOnlyCollection<Client> CreateClients(
        UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        var firstClient = new Client(
            new Name("Giant", "Bebra", "Lover"),
            new Rating(10),
            new PhoneNumber("88005553535"));

        var secondClient = new Client(
            new Name("Mega", "Bebra", "Lover"),
            new Rating(10),
            new PhoneNumber("88005553535"));

        var thirdClient = new Client(
            new Name("Omega", "Bebra", "Lover"),
            new Rating(10),
            new PhoneNumber("88005553535"));

        var firstUser = new ApplicationUser
        {
            Email = "Giant@bebra.love",
            UserName = "Giant Bebra",
            ModelId = firstClient.Id,
            ModelType = typeof(Client),
        };

        var secondUser = new ApplicationUser
        {
            Email = "Mega@bebra.love",
            UserName = "Mega Bebra",
            ModelId = secondClient.Id,
            ModelType = typeof(Client),
        };

        var thirdUser = new ApplicationUser
        {
            Email = "Omega@bebra.love",
            UserName = "Omega Bebra",
            ModelId = thirdClient.Id,
            ModelType = typeof(Client),
        };

        var userRole = new IdentityRole { Name = "User" };

        userManager.CreateAsync(firstUser, nameof(firstUser)).GetAwaiter().GetResult();
        userManager.CreateAsync(secondUser, nameof(secondUser)).GetAwaiter().GetResult();
        userManager.CreateAsync(thirdUser, nameof(thirdClient)).GetAwaiter().GetResult();

        roleManager.CreateAsync(userRole).GetAwaiter().GetResult();

        userManager.AddToRoleAsync(firstUser, userRole.Name);
        userManager.AddToRoleAsync(secondUser, userRole.Name);
        userManager.AddToRoleAsync(thirdUser, userRole.Name);

        return new[] { firstClient, secondClient, thirdClient };
    }
}