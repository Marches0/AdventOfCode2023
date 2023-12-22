using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day14;

internal class DishReader
{
    public Dish Read(string[] lines)
    {
        var items = lines
            .Select(l => l.Select(ReadItem).ToList())
            .ToList();

        return new Dish()
        {
            Content = items
        };
    }

    private DishContent ReadItem(char c)
    {
        return c switch
        {
            'O' => DishContent.RoundRock,
            '#' => DishContent.CubeRock,
            '.' => DishContent.Empty,
            _ => throw new NotImplementedException()
        };
    }
}

internal enum DishContent
{
    RoundRock = 1,
    CubeRock = 2,
    Empty = 3
}

internal class Dish
{
    public List<List<DishContent>> Content { get; set; }
}