namespace Bebruber.Utility.Extensions;

public static class SpanExtensions
{
    public static bool All<T>(this ReadOnlySpan<T> span, Func<T, bool> func)
    {
        foreach (T value in span)
        {
            if (!func.Invoke(value))
                return false;
        }

        return true;
    }

    public static bool None<T>(this ReadOnlySpan<T> span, Func<T, bool> func)
    {
        foreach (T value in span)
        {
            if (func.Invoke(value))
                return false;
        }

        return true;
    }
}