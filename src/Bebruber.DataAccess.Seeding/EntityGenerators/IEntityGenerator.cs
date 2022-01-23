using Microsoft.EntityFrameworkCore;

namespace Bebruber.DataAccess.Seeding.EntityGenerators;

public interface IEntityGenerator
{
    void Seed(ModelBuilder modelBuilder);
}