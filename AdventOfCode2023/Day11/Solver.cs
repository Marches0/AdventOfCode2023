namespace Day11;

public class Solver
{
    private const long ExpansionFactor = 1000000;

    public void Run()
    {
        Universe universe = new UniverseReader().Read(File.ReadAllLines("day11_input.txt"));
        Console.WriteLine(universe);

        GetDistances(universe);
    }

    private void GetDistances(Universe universe)
    {
        List<SpaceDistance> distances = new();

        List<SpaceItem> galaxies = universe.Items
            .Where(i => i.Type == SpaceType.Galaxy)
            .ToList();

        foreach ((SpaceItem galaxy, int index) in galaxies.Select((g, i) => (g, i)))
        {
            var newDistances = galaxies.Skip(index + 1)
                .Select(g => GetDistance(galaxy, g, universe))
                .ToList();

            distances.AddRange(newDistances);
        }

        Console.WriteLine(distances.Sum(d => d.Distance));
    }

    private SpaceDistance GetDistance(SpaceItem start, SpaceItem end, Universe universe)
    {
        // All shortest routes between galaxies will cross an equal number
        // of expansions, so just go across and down
        var startRow = Math.Min(start.Row, end.Row);
        var endRow = Math.Max(start.Row, end.Row);

        var startCol = Math.Min(start.Column, end.Column);
        var endCol = Math.Max(start.Column, end.Column);

        var rowTraversal = universe.Arrangement[startRow]
            .Skip(startCol + 1)
            .Take(endCol - startCol)
            .ToList();

        var colTraversal = universe.Arrangement
            .Select(r => r[endCol])
            .Skip(startRow + 1)
            .Take(endRow - startRow)
            .ToList();

        var distance = rowTraversal.Concat(colTraversal)
            .Sum(s => s is SpaceType.Expanded ? ExpansionFactor : 1);

        return new SpaceDistance()
        {
            Start = start,
            End = end,
            Distance = distance
        };
    }
}

internal class SpaceDistance
{
    public SpaceItem Start { get; set; }
    public SpaceItem End { get; set; }
    public long Distance { get; set; }

    public override string ToString()
    {
        return $"{Start.Id} -> {End.Id} {Distance}";
    }
}