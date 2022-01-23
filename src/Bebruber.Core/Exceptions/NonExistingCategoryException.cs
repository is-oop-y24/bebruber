namespace Bebruber.Core.Exceptions;

public class NonExistingCategoryException : Exception
{
    public NonExistingCategoryException()
        : base()
    {
    }

    public NonExistingCategoryException(string content)
        : base(content)
    {
    }

    public NonExistingCategoryException(string content, Exception innerException)
        : base(content, innerException)
    {
    }
}