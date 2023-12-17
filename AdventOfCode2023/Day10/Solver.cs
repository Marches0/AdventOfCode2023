using NetTopologySuite.Geometries;

namespace Day10;

public class Solver
{
    public void Run()
    {
        PipeTileArea tiles = new PipeTileReader().Read(File.ReadAllLines("day10_input.txt"));

        Part2(tiles);
    }

    private void Part1(PipeTileArea tiles)
    {
        var pipeline = GetPipeline(tiles);
        float distanceToGo = (float)pipeline.Count / 2; // probably shouldn't be odd, but just in case;

        Console.WriteLine(distanceToGo);
    }

    private void Part2(PipeTileArea tiles)
    {
        // Muahuahua
        List<Pipe> pipeline = GetPipeline(tiles);
        var closedPipeline = new List<Pipe>() { pipeline.Last() };
        closedPipeline.AddRange(pipeline);

        var coords = closedPipeline
            .Select(p => new Coordinate(p.X, p.Y));

        var poly = new Polygon(new LinearRing(coords.ToArray()));

        List<Geometry> pointsToCheck = tiles.Pipes
            .Where(p => !p.InMainLoop)
            .Select(p => new Point(p.X, p.Y))
            .ToList<Geometry>();

        var everything = new GeometryCollection(pointsToCheck.Concat(new[] { poly }).ToArray());
        Console.WriteLine(everything);

        var pointsInside = pointsToCheck
            .Where(poly.Contains)
            .ToList();

        var inside = new GeometryCollection(pointsInside.Concat(new[] {poly} ).ToArray());
        Console.WriteLine(inside);

        Console.WriteLine(pointsInside.Count);
    }

    private List<Pipe> GetPipeline(PipeTileArea pipeTileArea)
    {
        Pipe start = pipeTileArea.Pipes.First(p => p.Type == PipeType.AnimalStart);
        // Any connected direction
        // e.g. 
        /*
         *     |
         *    |S|
         *     |
         * The East/West NS pipes can't be reached, as you have to enter them from an N or S direction
         */
        Pipe current = start.Connections.Select(c => new
        {
            Connection = c,
            DirectionToGetTo = c.GetMovedDirection(start)
        })
        .Where(x => x.Connection.Directions.HasFlag(x.DirectionToGetTo))
        .First().Connection;
        Pipe last = start;

        List<Pipe> pipeParts = new List<Pipe>() { current};

        do
        {
            current.InMainLoop = true;
            PipeDirection movedDirection = last.GetMovedDirection(current);
            last = current;
            current = current.GetNextPipe(movedDirection);
            pipeParts.Add(current);

        } while (current != start);

        return pipeParts;
    }
}
