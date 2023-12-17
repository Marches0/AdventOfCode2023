using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day8;

internal class DesertMapReader
{
    public DesertMap Read(string[] lines)
    {
        var tiles = lines.Skip(2)
            .Select(GetRawTile)
            .ToDictionary(t => t.Name);

        // Fill out the left/right on the tiles
        // directly so we can avoid the dictionary later
        foreach (var tile in tiles)
        {
            tile.Value.Left = tiles[tile.Value.LeftName];
            tile.Value.Right = tiles[tile.Value.RightName];
        }

        
        return new DesertMap()
        {
            Directions = GetDirections(lines[0]),
            Tiles = tiles
        };
    }

    private DesertTile GetRawTile(string line)
    {
        var destinations = line
            .SkipWhile(l => l != '(')
            .Skip(1)
            .SkipLast(1)
            .CollapseToString()
            .Split(", ")
            .ToList();

        return new DesertTile()
        {
            Name = line[0..3],
            LeftName = destinations[0],
            RightName = destinations[1]
        };
    }

    private List<Direction> GetDirections(string line)
    {
        return line
            .Select(c => c == 'R' ? Direction.Right : Direction.Left)
            .ToList();
    }
}

internal class DesertMap
{
    public List<Direction> Directions { get; set; }
    public Dictionary<string, DesertTile> Tiles { get; set; }
}

internal class DesertTile
{
    public string Name { get; set; }
    public DesertTile Left { get; set; }
    public DesertTile Right { get; set; }

    public string LeftName { get; set; }
    public string RightName { get; set; }

    public DesertTile Travel(Direction direction)
    {
        return direction == Direction.Right
            ? Right
            : Left;
    }
}

public enum Direction
{
    Right = 1,
    Left = 2
}