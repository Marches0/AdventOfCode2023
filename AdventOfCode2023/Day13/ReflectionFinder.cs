using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day13;

internal class ReflectionFinder
{
    /// <summary>
    ///  How many possible smudges we want to find. 0 = get exact match (part 1)
    /// </summary>
    private const int SmudgeCandidates = 1;

    public Reflection FindReflection(Valley valley)
    {
        ReflectionOrientation orientation = ReflectionOrientation.Row;
        int? reflection = FindRowReflection(valley.Content);

        if (reflection is null)
        {
            // rotate the contents so we can
            // reuse our logic
            orientation = ReflectionOrientation.Column;
            
            // number magically works despite the rotation!
            reflection = FindRowReflection(valley.Content.Rotate().ToList());
        }

        if (reflection is null)
        {
            throw new Exception("uh oh");
        }

        return new Reflection()
        {
            Orientation = orientation,
            RowCol = reflection.Value
        };
    }

    private int? FindRowReflection(
        List<List<ValleyContent>> content)
    {
        List<(int, int)> differentIndexes = new();

        // Can't be a reflection if it's right on the end
        for (int i = 1; i < content.Count; i++)
        {
            // i = the start of the second reflection
            int reflectionLength = Math.Min(i, content.Count - i);
            int firstStart = i - reflectionLength;

            // Both start from the reflection location being checked
            // and go out
            List<List<ValleyContent>> firstPart = content.Skip(firstStart)
                .Take(reflectionLength)
                .Reverse()
                .ToList();

            List<List<ValleyContent>> secondPart = content.Skip(i)
                .Take(reflectionLength)
                .ToList();

            // These indexes are local to firstPart and secondPart.
            // firstPart is also REVERSED; ([last], 0) becomes ([first], 0)
            // They need to be adjusted to be relative to content.
            var localDifferences = GetDifferentPositions(firstPart, secondPart);
            var actual = localDifferences
                .Select(d => (reflectionLength - d.Item1 - 1, d.Item2))
                .ToList();

            if (actual.Count == SmudgeCandidates)
            {
                return i;
            }
        }

        return null;
    }

    private List<(int, int)> GetDifferentPositions(
        List<List<ValleyContent>> firstPart,
        List<List<ValleyContent>> secondPart)
    {
        List<(int, int)> differentPositions = new();

        for (int i = 0; i < firstPart.Count; i++)
        {
            // Get the number of differences between these two, and the positions
            // those differences are in.
            // Must be 
            List<int> differences = GetDifferentIndexes(firstPart[i], secondPart[i]);
            differentPositions.AddRange(differences.Select(d => (i, d)));
        }

        return differentPositions;
    }

    private List<int> GetDifferentIndexes(List<ValleyContent> a, List<ValleyContent> b)
    {
        List<int> different = new();

        foreach (var (pair, i) in a.Zip(b).Select((pair, i) => (pair, i)))
        {
            if (pair.First != pair.Second)
            {
                different.Add(i);
            }
        }

        return different;
    }
}

internal enum ReflectionOrientation
{
    Column = 1,
    Row = 2,
}

internal class Reflection
{
    public ReflectionOrientation Orientation { get; set; }

    public int RowCol { get; set; }
}