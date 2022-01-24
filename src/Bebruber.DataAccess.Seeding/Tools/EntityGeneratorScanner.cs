using System.Reflection;
using Bebruber.DataAccess.Seeding.EntityGenerators;
using Bebruber.Identity;
using Bebruber.Utility.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Bebruber.DataAccess.Seeding.Tools;

public static class EntityGeneratorScanner
{
    public static IReadOnlyCollection<IEntityGenerator> GetEntityGeneratorsFromAssembly(
        UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, params Type[] types)
        => GetEntityGeneratorsFromAssembly(userManager, roleManager, types.Select(t => t.Assembly).ToArray());

    public static IReadOnlyCollection<IEntityGenerator> GetEntityGeneratorsFromAssembly(
        UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, params Assembly[] assemblies)
    {
        var collection = new ServiceCollection();
        collection.AddSingleton(userManager);
        collection.AddSingleton(roleManager);

        var types = assemblies
            .SelectMany(a => a.DefinedTypes)
            .Where(t => t.IsAssignableTo(typeof(IEntityGenerator)))
            .Where(t => !t.IsAbstract && !t.IsInterface)
            .ToList();

        foreach (Type type in types)
        {
            collection.AddSingleton(type);
        }

        ServiceProvider provider = collection.BuildServiceProvider();

        return types.Select(t => (IEntityGenerator)provider.GetRequiredService(t)).ToList();
    }
}