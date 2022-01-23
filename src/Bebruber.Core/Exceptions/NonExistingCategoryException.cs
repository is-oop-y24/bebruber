using Bebruber.Domain.Enumerations;
using Bebruber.Domain.Tools;

namespace Bebruber.Core.Exceptions;

public class NonExistingCategoryException : BebruberException
{
    public NonExistingCategoryException(CarCategory category)
        : base($"{nameof(CarCategory)} {category} does not exists")
    {
    }
}