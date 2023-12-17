namespace Day10;

public class Solver
{
    public void Run()
    {
        PipeTileArea tiles = new PipeTileReader().Read(File.ReadAllLines("day10_input.txt"));
        int len = GetAnimalPipeLength(tiles);
        float distanceToGo = (float)len / 2; // probably shouldn't be odd, but just in case;

        Console.WriteLine(distanceToGo);
    }

    private int GetAnimalPipeLength(PipeTileArea pipeTileArea)
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
        int distance = 1;

        do
        {   
            PipeDirection movedDirection = last.GetMovedDirection(current);
            last = current;
            current = current.GetNextPipe(movedDirection);
            ++distance;

        } while (current != start);

        return distance;
    }
}
