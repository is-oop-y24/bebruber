using System;
using Bebruber.Domain.Tools;
using Bebruber.Domain.ValueObjects.Exceptions;
using Bebruber.Utility.Extensions;

namespace Bebruber.Domain.ValueObjects;

public sealed class Name : ValueObject<Name>
{
    public Name(string firstName, string? middleName, string lastName)
    {
        Validate(nameof(FirstName), firstName);
        Validate(nameof(MiddleName), middleName);
        Validate(nameof(LastName), lastName);

        FirstName = firstName;
        MiddleName = middleName ?? string.Empty;
        LastName = lastName;
    }

    public string FirstName { get; private init; }
    public string MiddleName { get; private init; }
    public string LastName { get; private init; }

    public override int GetHashCode()
        => HashCode.Combine(FirstName, MiddleName, LastName);

    protected override bool EqualTo(Name other)
        => other.FirstName.Equals(FirstName) &&
           other.MiddleName.Equals(MiddleName) &&
           other.LastName.Equals(LastName);

    private static void Validate(string componentName, string? componentValue)
    {
        if (string.IsNullOrEmpty(componentValue) || !componentValue.AsSpan().All(char.IsLetter))
            throw new InvalidClientNameComponentException(componentName, componentValue);
    }
}