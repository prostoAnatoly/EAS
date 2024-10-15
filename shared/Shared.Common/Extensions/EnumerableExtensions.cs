namespace Shared.Common.Extensions;

public static class EnumerableExtensions
{
    public static IEnumerable<T> EmptyIfNull<T>(this IEnumerable<T>? enumerable)
    {
        return enumerable ?? [];
    }

    public static bool SequenceEqualWithNull<TItem>(this IEnumerable<TItem>? first, IEnumerable<TItem>? second)
    {
        if (first == second) { return true; }
        
        if (first == null && second == null) { return true; }

        if (first != null && second != null && Enumerable.SequenceEqual(first, second)) { return true; }

        return false;
    }

    public static IEnumerable<T> WhereIf<T>(this IEnumerable<T> source, bool condition, Func<T, bool> predicate)
    {
        return condition
            ? source.Where(predicate)
            : source;
    }
}
