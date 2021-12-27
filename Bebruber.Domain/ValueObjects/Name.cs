using System.Collections.Generic;
using System.Text.RegularExpressions;
using Bebruber.Domain.Tools;
using Bebruber.Domain.ValueObjects.Exceptions;

namespace Bebruber.Domain.ValueObjects;

public sealed class Name : ValueObject<Name>
{
    public Name(string firstName, string? middleName, string lastName)
    {
        if (string.IsNullOrEmpty(firstName) || !Regex.IsMatch(firstName))
            throw new InvalidClientNameComponentException(nameof(FirstName), firstName);

        if (string.IsNullOrEmpty(lastName) || !Regex.IsMatch(lastName))
            throw new InvalidClientNameComponentException(nameof(LastName), lastName);

        if (middleName is not null && !Regex.IsMatch(middleName))
            throw new InvalidClientNameComponentException(nameof(MiddleName), middleName);

        FirstName = firstName;
        MiddleName = middleName ?? string.Empty;
        LastName = lastName;
    }

    public static Regex Regex { get; } = new Regex(@"[a-zа-я\-]+", RegexOptions.Compiled);

    public string FirstName { get; private init; }
    public string MiddleName { get; private init; }
    public string LastName { get; private init; }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return FirstName;
        yield return MiddleName;
        yield return LastName;
    }
}