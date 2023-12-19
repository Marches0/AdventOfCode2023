using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day13;
internal class ValleyReader
{
    public Valley Read(string[] lines)
    {
        return new Valley()
        {
            Content = lines
                .Select(l => l.Select(GetValleyContent).ToList())
                .ToList()
        };
    }

    private ValleyContent GetValleyContent(char c)
    {
        return c switch
        {
            '.' => ValleyContent.Ash,
            '#' => ValleyContent.Rocks,
            _ => throw new NotImplementedException()
        };
    }
}

internal enum ValleyContent
{
    Ash = 1,
    Rocks = 2
}

internal class Valley
{
    public List<List<ValleyContent>> Content { get; set; }
}