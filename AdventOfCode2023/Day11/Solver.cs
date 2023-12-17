namespace Day11;

public class Solver
{
    public void Run()
    {
        Universe universe = new UniverseReader().Read(File.ReadAllLines("day11_input.txt"));
        Console.WriteLine(universe);

        Part1(universe);
    }

    private void Part1(Universe universe)
    {
        List<SpaceDistance> distances = new();

        List<SpaceItem> galaxies = universe.Items
            .Where(i => i.Type == SpaceType.Galaxy)
            .ToList();

        foreach ((SpaceItem galaxy, int index) in galaxies.Select((g, i) => (g, i)))
        {
            var newDistances = galaxies.Skip(index + 1)
                .Select(g => new SpaceDistance()
                {
                    Start = galaxy,
                    End = g,
                    Distance = Math.Abs(g.Row - galaxy.Row) + Math.Abs(g.Column - galaxy.Column)
                })
                .ToList();

            distances.AddRange(newDistances);
        }

        Console.WriteLine(distances.Sum(d => d.Distance));
    }
}

internal class SpaceDistance
{
    public SpaceItem Start { get; set; }
    public SpaceItem End { get; set; }
    public int Distance { get; set; }

    public override string ToString()
    {
        return $"{Start.Id} -> {End.Id} {Distance}";
    }
}