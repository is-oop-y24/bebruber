using System.Collections.Generic;
using System.Text.RegularExpressions;
using Bebruber.Domain.Tools;
using Bebruber.Domain.ValueObjects.Exceptions;

namespace Bebruber.Domain.ValueObjects.User;

public sealed class Name : ValueObject<Name>
{
    public Name(string firstName, string? middleName, string lastName)
    {
        if (!Regex.IsMatch(firstName))
            throw new InvalidClientNameComponentException(nameof(FirstName), firstName);

        if (!Regex.IsMatch(lastName))
            throw new InvalidClientNameComponentException(nameof(LastName), lastName);

        if (middleName is not null && !Regex.IsMatch(middleName))
            throw new InvalidClientNameComponentException(nameof(MiddleName), middleName);

        FirstName = firstName;
        MiddleName = middleName ?? string.Empty;
        LastName = lastName;
    }

    public static Regex Regex { get; } = new Regex(@"[a-zа-я\-]+", RegexOptions.IgnoreCase | RegexOptions.Compiled);

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