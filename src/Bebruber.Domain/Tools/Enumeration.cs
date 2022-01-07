using System;
using Bebruber.Utility.Extensions;

namespace Bebruber.Domain.Tools;

public abstract class Enumeration<TValue, TEnumeration> : IEquatable<TEnumeration>
    where TEnumeration : Enumeration<TValue, TEnumeration>
{
    protected Enumeration(string name, TValue value)
    {
        Name = name.ThrowIfNull();
        Value = value;
    }

    public string Name { get; private init; }
    public TValue Value { get; private init; }

    public static bool operator ==(Enumeration<TValue, TEnumeration> left, Enumeration<TValue, TEnumeration> right)
    {
        if ((left, right) is (null, null))
            return true;

        return left?.Equals(right) ?? false;
    }

    public static bool operator !=(Enumeration<TValue, TEnumeration> left, Enumeration<TValue, TEnumeration> right)
        => !(left == right);

    public bool Equals(TEnumeration? other)
        => other is not null && (other.Value?.Equals(Value) ?? false);

    public override bool Equals(object? obj)
        => Equals(obj as TEnumeration);

    public override int GetHashCode()
        => Value?.GetHashCode() ?? 0;

    public override string ToString()
        => Name;
}