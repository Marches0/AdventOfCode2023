﻿namespace Shared;

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