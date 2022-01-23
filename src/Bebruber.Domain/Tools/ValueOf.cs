using System;
using System.Collections.Generic;

namespace Bebruber.Domain.Tools;

public class ValueOf<T, TObject> : ValueObject<TObject>
    where TObject : ValueOf<T, TObject>
{
    protected ValueOf(T value)
    {
        Value = value;
    }

    protected ValueOf(T value, Func<T, bool> validator, Exception exception)
    {
        if (!validator.Invoke(value))
            throw exception;

        Value = value;
    }

    public T Value { get; private init; }

    public override string ToString()
        => Value?.ToString() ?? string.Empty;

    protected sealed override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }
}