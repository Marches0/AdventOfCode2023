using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day13;

internal class ReflectionFinder
{
    public Reflection FindReflection(Valley valley)
    {
        ReflectionOrientation orientation = ReflectionOrientation.Row;
        int? reflection = FindRowReflection(valley.Content);

        if(reflection is null)
        {
            // rotate the contents so we can
            // reuse our logic
            orientation = ReflectionOrientation.Column;
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

    private int? FindRowReflection(List<List<ValleyContent>> content)
    {
        // Can't be a reflection if it's right on the end
        for (int i = 1; i < content.Count; i++)
        {
            // i = the start of the second reflection
            int reflectionLength = Math.Min(i, content.Count - i);
            int firstStart = i - reflectionLength;

            // Both start from the reflection location being checked
            // and go out
            var firstPart = content.Skip(firstStart)
                .Take(reflectionLength)
                .Reverse()
                .ToList();

            var secondPart = content.Skip(i)
                .Take(reflectionLength)
                .ToList();

            if (firstPart.Zip(secondPart).All(x => x.First.SequenceEqual(x.Second)))
            {
                return i;
            }
        }

        return null;
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