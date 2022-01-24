using System;
using System.ComponentModel.DataAnnotations;
using Bebruber.Domain.Tools;

namespace Bebruber.Domain.ValueObjects.User;

public class Email : ValueOf<string, Email>
{
    public Email(string value)
        : base(value, s => new DataTypeAttribute(DataType.EmailAddress).IsValid(s), new Exception("Wrong email format"))
    { }
}