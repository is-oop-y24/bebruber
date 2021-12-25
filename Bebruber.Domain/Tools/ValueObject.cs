using System;

namespace Bebruber.Domain.Tools;

public abstract class ValueObject<TObject> : IEquatable<TObject>
    where TObject : ValueObject<TObject>
{
    public static bool operator ==(ValueObject<TObject>? left, ValueObject<TObject>? right)
        => left?.Equals(right) ?? false;

    public static bool operator !=(ValueObject<TObject>? left, ValueObject<TObject>? right)
        => !(left == right);

    public bool Equals(TObject? other)
        => other is not null && EqualTo(other);

    public override bool Equals(object? obj)
        => Equals(obj as TObject);

    public abstract override int GetHashCode();
    protected abstract bool EqualTo(TObject other);
}