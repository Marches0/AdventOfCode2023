namespace Shared;

public static class EnumerableCharExtensions
{
    public static string CollapseToString(this IEnumerable<char> value)
    {
        return new string(value.ToArray());
    }    
}

public static class EnumerableStringExtensions
{
    public static IEnumerable<string[]> ChunkByNewline(this IEnumerable<string> values)
    {
        List<string> current = new();

        foreach (string value in values)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                yield return current.ToArray();
                current = new();
            }
            else
            {
                current.Add(value);
            }
        }

        yield return current.ToArray();
    }
}

public static class EnumerableExtensions
{
    public static IEnumerable<List<T>> Rotate<T>(this IEnumerable<IEnumerable<T>> source)
    {
        var enumerators = source.Select(e => e.GetEnumerator()).ToArray();
        try
        {
            while (enumerators.All(e => e.MoveNext()))
            {
                yield return enumerators.Select(e => e.Current).ToList();
            }
        }
        finally
        {
            Array.ForEach(enumerators, e => e.Dispose());
        }
    }
}

public static class StringExtensions
{
    public static string GetFirstNumber(this string value)
    {
        return value
            .SkipWhile(c => !char.IsDigit(c))
            .TakeWhile(c => char.IsDigit(c))
            .CollapseToString();
    }

    public static List<long> GetNumbers(this string value, string delimiter)
    {
        return value.Split(delimiter, StringSplitOptions.RemoveEmptyEntries)
            .Select(long.Parse)
            .ToList();
    }

    public static string SkipPastColon(this string value)
    {
        return value.SkipWhile(c => c != ':')
            .Skip(1)
            .CollapseToString();
    }
}

public static class IntExtensions
{
    public static int IndexClamp(this int index, int max)
    {
        if (index < 0)
        {
            return 0;
        }

        if (index >= max)
        {
            return max - 1;
        }

        return index;
    }

    public static bool InIndexRange(this int index, int max)
    {
        if (index < 0)
        {
            return false;
        }

        if (index > max)
        {
            return false;
        }

        return true;
    }
}