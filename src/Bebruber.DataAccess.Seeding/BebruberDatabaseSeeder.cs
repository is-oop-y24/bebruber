using Bebruber.DataAccess.Seeding.EntityGenerators;
using Bebruber.DataAccess.Seeding.Tools;
using Bebruber.Utility.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Bebruber.DataAccess.Seeding;

public class BebruberDatabaseSeeder
{
    private readonly IReadOnlyCollection<IEntityGenerator> _generators;
    private readonly ModelBuilder _modelBuilder;

    public BebruberDatabaseSeeder(ModelBuilder modelBuilder, IServiceCollection collection)
    {
        _modelBuilder = modelBuilder;
        _generators = EntityGeneratorScanner.GetEntityGeneratorsFromAssembly(collection, typeof(IAssemblyMarker));
    }

    public void Seed()
    {
        _generators.ForEach(g => g.Seed(_modelBuilder));
    }
}