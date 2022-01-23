using System;
using System.Collections.Generic;
using System.Linq;

namespace Bebruber.Domain.Tools;

public abstract class ValueObject<TObject> : IEquatable<TObject>
    where TObject : ValueObject<TObject>
{
    public static bool operator ==(ValueObject<TObject>? left, ValueObject<TObject>? right)
    {
        if ((left, right) is (null, null))
            return true;

        return left?.Equals(right) ?? false;
    }

    public static bool operator !=(ValueObject<TObject>? left, ValueObject<TObject>? right)
        => !(left == right);

    public bool Equals(TObject? other)
        => other is not null &&
           other.GetEqualityComponents().SequenceEqual(GetEqualityComponents());

    public override bool Equals(object? obj)
        => Equals(obj as TObject);

    public override int GetHashCode()
    {
        return GetEqualityComponents()
            .Select(x => x?.GetHashCode() ?? 0)
            .Aggregate(HashCode.Combine);
    }

    public override string ToString()
    {
        IEnumerable<string?> strings = GetEqualityComponents().Select(x => x?.ToString());
        return string.Join(", ", strings);
    }

    protected abstract IEnumerable<object?> GetEqualityComponents();
}