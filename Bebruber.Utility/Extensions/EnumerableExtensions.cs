namespace Bebruber.Utility.Extensions;

public static class EnumerableExtensions
{
    public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
    {
        foreach (var value in enumerable)
        {
            action.Invoke(value);
        }
    }
}