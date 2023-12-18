using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day12;

internal class HotSpringReader
{
    public SpringRow Read(string line)
    {
        // #.#.### 1,1,3
        var parts = line.Split(" ");
        return new SpringRow()
        {
            Statuses = parts[0].Select(GetStatus).ToList(),
            DamagedGroups = parts[1].Split(',').Select(int.Parse).ToList()
        };
    }

    private SpringStatus GetStatus(char c)
    {
        return c switch
        {
            '.' => SpringStatus.Operational,
            '#' => SpringStatus.Damaged,
            '?' => SpringStatus.Unknown,
            _ => throw new NotImplementedException()
        };
    }
}

internal enum SpringStatus
{
    Operational = 1,
    Damaged = 2,
    Unknown = 3,
}

internal class SpringRow
{
    public List<SpringStatus> Statuses { get; set; }
    public List<int> DamagedGroups { get; set; }

    public override string ToString()
    {
        return $"{string.Join("", Statuses.Select(StatusString))} {string.Join(",", DamagedGroups)}";
    }

    private string StatusString(SpringStatus status)
    {
        return status switch
        {
            SpringStatus.Operational => ".",
            SpringStatus.Damaged => "#",
            SpringStatus.Unknown => "?",
            _ => throw new NotImplementedException()
        };
    }
}
