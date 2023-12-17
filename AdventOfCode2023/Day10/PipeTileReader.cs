using Shared;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Day10;

internal class PipeTileReader
{
    public PipeTileArea Read(string[] lines)
    {
        var pipes = lines
            .Select(l => l.Select(GetPipe).ToList())
            .ToList();

        // Number the pipes first so they all have data
        // before we attempt to use it
        for (int y = 0; y < pipes.Count; y++)
        {
            for (int x = 0; x < pipes[y].Count; x++)
            {
                pipes[y][x].X = x;
                pipes[y][x].Y = y;
            }
        }

        int yMax = pipes.Count - 1;
        for (int y = 0; y <= yMax; y++)
        {
            int xMax = pipes[y].Count - 1;
            for (int x = 0; x <= xMax; x++)
            {
                Pipe current = pipes[y][x];
                
                current.Connections = GetConnectionOffsets(current.Directions)
                    .Select(d => (x: x + d.x, y: y + d.y)) // turn offsets into target positions
                    .Where(p => p.x.InIndexRange(xMax) && p.y.InIndexRange(yMax))
                    .Select(p => pipes[p.y][p.x])
                    .ToList();
            }
        }

        return new PipeTileArea()
        {
            Pipes = pipes
                .SelectMany(p => p)
                .ToList(),
        };
    }

    private Pipe GetPipe(char tile)
    {
        var type = GetTileContent(tile);
        var direction = GetConnectionDirections(type);

        return new Pipe()
        {
            Type = type,
            Directions = direction
        };
    }

    private PipeType GetTileContent(char tile)
    {
        return tile switch
        {
            '.' => PipeType.None,
            'S' => PipeType.AnimalStart,
            '-' => PipeType.WestEast,
            '|' => PipeType.NorthSouth,
            'L' => PipeType.NorthEast,
            'J' => PipeType.NorthWest,
            '7' => PipeType.SouthWest,
            'F' => PipeType.SouthEast,
            _ => throw new NotImplementedException()
        };
    }

    private PipeDirection GetConnectionDirections(PipeType pipeType)
    {
        return pipeType switch
        {
            PipeType.None => PipeDirection.None,
            PipeType.NorthSouth => PipeDirection.North | PipeDirection.South,
            PipeType.WestEast => PipeDirection.West | PipeDirection.East,
            PipeType.NorthEast => PipeDirection.North | PipeDirection.East,
            PipeType.NorthWest => PipeDirection.North | PipeDirection.West,
            PipeType.SouthWest => PipeDirection.South | PipeDirection.West,
            PipeType.SouthEast => PipeDirection.South | PipeDirection.East,
            PipeType.AnimalStart => PipeDirection.North | PipeDirection.South | PipeDirection.West | PipeDirection.East,
            _ => throw new NotImplementedException()
        };
    }

    private List<(int x, int y)> GetConnectionOffsets(PipeDirection pipeDirection)
    {
        List<(int x, int y)> offsets = new();

        if(pipeDirection.HasFlag(PipeDirection.North))
        {
            offsets.Add((0, -1));
        }

        if (pipeDirection.HasFlag(PipeDirection.South))
        {
            offsets.Add((0, 1));
        }

        if (pipeDirection.HasFlag(PipeDirection.West))
        {
            offsets.Add((-1, 0));
        }

        if (pipeDirection.HasFlag(PipeDirection.East))
        {
            offsets.Add((1, 0));
        }

        return offsets;
    }
}

internal enum PipeType
{
    None = 0,
    NorthSouth = 1,
    WestEast = 2,
    NorthEast = 3,
    NorthWest = 4,
    SouthWest = 5,
    SouthEast = 6,
    AnimalStart = 7
}

[Flags]
internal enum PipeDirection
{
    None = 0,
    North = 1,
    South = 2,
    West = 4,
    East = 8
}

internal class Pipe
{
    public PipeType Type { get; set; }

    public PipeDirection Directions { get; set; }

    public List<Pipe> Connections { get; set; } = new List<Pipe>();

    public int X { get; set; }
    public int Y { get; set; }

    public PipeDirection GetMovedDirection(Pipe end)
    {
        (int X, int Y) movement = (X: end.X - this.X, Y: end.Y - this.Y);

        if(movement.Y == 1)
        {
            return PipeDirection.South;
        }
        else if (movement.Y == -1)
        {
            return PipeDirection.North;
        }
        else if (movement.X == 1)
        {
            return PipeDirection.East;
        }
        else if (movement.X == -1)
        {
            return PipeDirection.West;
        }

        throw new InvalidOperationException();
    }

    public Pipe GetNextPipe(PipeDirection fromDirection)
    {
        // Invert from direction - if we enter an NS pipe from S,
        // then its N end is consumed.

        // If we have an NE connection and came from E,
        // return the NE because that is going in the same direction

        // But we need to be careful to not backtrack
        // e.g. Going North from NS into another NS; we
        // want the one that is not in the direction we just came from

        var xxx = Connections.Select(c => new
        {
            Connection = c,
            GettingToThereFromDirection = c.GetMovedDirection(this)
        }).ToList();

        return Connections
            //.Where(c => c.Directions.HasFlag(fromDirection))
            .Single(c => c.GetMovedDirection(this) != fromDirection);
    }

    public override string ToString()
    {
        return $"{Type} ({X}, {Y}) [{Connections.Count}]";
    }
}

internal class PipeTileArea
{
    public List<Pipe> Pipes { get; set; }
}