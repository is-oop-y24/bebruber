using Bebruber.DataAccess.Seeding.EntityGenerators;
using Bebruber.DataAccess.Seeding.Tools;
using Bebruber.Identity;
using Bebruber.Utility.Extensions;
using Microsoft.AspNetCore.Identity;

namespace Bebruber.DataAccess.Seeding;

public class BebruberDatabaseSeeder
{
    private readonly IReadOnlyCollection<IEntityGenerator> _generators;

    public BebruberDatabaseSeeder(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _generators = EntityGeneratorScanner
            .GetEntityGeneratorsFromAssembly(userManager, roleManager, typeof(IAssemblyMarker));
    }

    public void Seed(BebruberDatabaseContext context)
    {
        _generators.ForEach(g => g.Seed(context));
        context.SaveChanges();
    }
}