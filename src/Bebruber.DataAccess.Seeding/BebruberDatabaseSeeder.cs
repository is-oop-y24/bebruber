using Bebruber.DataAccess.Seeding.EntityGenerators;
using Bebruber.DataAccess.Seeding.Tools;
using Bebruber.Utility.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Bebruber.DataAccess.Seeding;

public class BebruberDatabaseSeeder
{
    private readonly IReadOnlyCollection<IEntityGenerator> _generators;

    public BebruberDatabaseSeeder(IServiceCollection collection)
    {
        _generators = EntityGeneratorScanner.GetEntityGeneratorsFromAssembly(collection, typeof(IAssemblyMarker));
    }

    public void Seed(ModelBuilder modelBuilder)
    {
        _generators.ForEach(g => g.Seed(modelBuilder));
    }
}