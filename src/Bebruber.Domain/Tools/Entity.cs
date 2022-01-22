using System;

namespace Bebruber.Domain.Tools;

public abstract class Entity<TEntity> : IEquatable<TEntity>
    where TEntity : Entity<TEntity>
{
    protected Entity()
    {
        Id = Guid.NewGuid();
    }

    public Guid Id { get; protected init; }

    public bool Equals(TEntity? other)
        => other is not null && other.Id.Equals(Id);

    public sealed override bool Equals(object? obj)
        => Equals(obj as TEntity);

    public sealed override int GetHashCode()
        => Id.GetHashCode();
}