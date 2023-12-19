using Shared;

namespace Day13;

public class Solver
{
    public void Run()
    {
        var reader = new LavaReader();

        var lavaSites = File.ReadAllLines("test.txt")
            .ChunkByNewline()
            .Select(reader.Read)
            .ToList();
    }
}