using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Bebruber.Domain.Tools;
using Bebruber.Domain.ValueObjects.Exceptions;

namespace Bebruber.Domain.ValueObjects.User;

public class PhoneNumber : ValueOf<string, PhoneNumber>
{
    protected PhoneNumber(string value)
        : base(
            value,
            s => new DataTypeAttribute(DataType.PhoneNumber).IsValid(s),
            new InvalidPhoneNumberException(value))
    { }
}