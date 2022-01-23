using Bebruber.Domain.Tools;

namespace Bebruber.Domain.ValueObjects.User;

public class PasswordHash : ValueOf<string, PasswordHash>
{
    public PasswordHash(string value)
        : base(value) { }

    protected PasswordHash() { }
}