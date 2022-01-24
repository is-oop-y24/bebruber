using Bebruber.Domain.Entities;
using Bebruber.Domain.ValueObjects.User;
using Bebruber.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Bebruber.DataAccess.Seeding.EntityGenerators;

public class ClientGenerator : IEntityGenerator
{
    public ClientGenerator(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        Clients = CreateClients(userManager, roleManager);
    }

    public IReadOnlyList<Client> Clients { get; }

    public void Seed(BebruberDatabaseContext context)
    {
        context.Clients.AddRange(Clients);
    }

    private static IReadOnlyList<Client> CreateClients(
        UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        var firstClient = new Client(
            new Name("Giant", "Bebra", "Lover"),
            new Rating(10),
            new PhoneNumber("88005553535"),
            new Email("Giant@bebra.love"));

        var secondClient = new Client(
            new Name("Mega", "Bebra", "Lover"),
            new Rating(10),
            new PhoneNumber("88005553535"),
            new Email("Mega@bebra.love"));

        var thirdClient = new Client(
            new Name("Omega", "Bebra", "Lover"),
            new Rating(10),
            new PhoneNumber("88005553535"),
            new Email("Omega@bebra.love"));

        var firstUser = new ApplicationUser
        {
            Email = firstClient.Email.Value,
            UserName = "GiantBebra",
            ModelId = firstClient.Id,
            ModelType = typeof(Client),
        };

        var secondUser = new ApplicationUser
        {
            Email = secondClient.Email.Value,
            UserName = "MegaBebra",
            ModelId = secondClient.Id,
            ModelType = typeof(Client),
        };

        var thirdUser = new ApplicationUser
        {
            Email = thirdClient.Email.Value,
            UserName = "OmegaBebra",
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