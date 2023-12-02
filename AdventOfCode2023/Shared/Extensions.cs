namespace Shared;

public static class EnumerableCharExtensions
{
    public static string CollapseToString(this IEnumerable<char> value)
    {
        return new string(value.ToArray());
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
}