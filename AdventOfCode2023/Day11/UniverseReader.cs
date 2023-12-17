using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Day11;

internal class UniverseReader
{
    public Universe Read(string[] lines) 
    {
        var tiles = lines
            .Select(l => l.Select(GetContent).ToList())
            .ToList();

        var expanded = Expand(tiles);

        return new Universe()
        {
            Arrangement = expanded,
            Items = GetSpaceItems(expanded)
        };
    }

    private List<List<SpaceType>> Expand(List<List<SpaceType>> startArrangement)
    {
        // Every row and every column that contains only empty space
        // should be duplicated
        // Go backwards so we can edit in-place
        for (int i = startArrangement.Count - 1; i >= 0; i--)
        {
            if (startArrangement[i].All(c => c == SpaceType.Empty))
            {
                startArrangement[i] = startArrangement[i]
                    .Select(_ => SpaceType.Expanded)
                    .ToList();
            }
        }

        for (int i = startArrangement[0].Count -1; i >= 0; i--)
        {
            if (ColumnIsEmpty(startArrangement, i))
            {
                foreach (var row in startArrangement)
                {
                    row[i] = SpaceType.Expanded;
                }
            }
        }

        return startArrangement;
    }

    private List<SpaceType> EmptyRow(int length)
    {
        return Enumerable.Range(0, length)
            .Select(_ => SpaceType.Empty)
            .ToList();
    }

    private SpaceType GetContent(char tile)
    {
        return tile switch
        {
            '.' => SpaceType.Empty,
            '#' => SpaceType.Galaxy,
            _ => throw new NotImplementedException()
        };
    }

    private bool ColumnIsEmpty(List<List<SpaceType>> space, int index)
    {
        return space
            .Select(s => s[index])
            .All(c => c == SpaceType.Empty || c == SpaceType.Expanded);
    }

    private List<SpaceItem> GetSpaceItems(List<List<SpaceType>> arrangement)
    {
        List<SpaceItem> items = new();
        int galaxyId = 1;

        for (int i = 0; i < arrangement.Count; i++)
        {
            for (int j = 0; j < arrangement[i].Count; j++)
            {
                SpaceType currentType = arrangement[i][j];
                items.Add(new SpaceItem()
                {
                    Id = currentType == SpaceType.Galaxy
                        ? galaxyId++
                        : 0,
                    Type = currentType,
                    Row = i,
                    Column = j
                });
            }
        }

        return items;
    }
}

internal enum SpaceType
{
    Empty = 1,
    Galaxy = 2,
    Expanded = 3,
}

internal class SpaceItem
{
    public int Id { get; set; }
    public SpaceType Type { get; set; }
    public int Row { get; set; }
    public int Column { get; set; }

    public override string ToString()
    {
        return $"{Id} ({Column}, {Row})";
    }
}

internal class Universe
{
    public List<List<SpaceType>> Arrangement { get; set; }

    public List<SpaceItem> Items { get; set; }

    public override string ToString()
    {
        var lines = Arrangement
            .Select(c => string.Join(' ', c.Select(ContentString)))
            .ToList();

        return string.Join("\r\n", lines);
    }

    private string ContentString(SpaceType content)
    {
        return content == SpaceType.Galaxy
            ? "#"
            : ".";
    }
}