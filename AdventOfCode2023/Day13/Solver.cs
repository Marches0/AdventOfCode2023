using Shared;

namespace Day13;

public class Solver
{
    public void Run()
    {
        var reader = new ValleyReader();

        List<Valley> valleys = File.ReadAllLines("day13_input.txt")
            .ChunkByNewline()
            .Select(reader.Read)
            .ToList();

        var finder = new ReflectionFinder();
        List<Reflection> reflections = valleys
            .Select(finder.FindReflection)
            .ToList();

        var total = reflections
            .Sum(r => r.Orientation == ReflectionOrientation.Column
            ? r.RowCol
            : r.RowCol * 100);

        Console.WriteLine(total);
    }
}