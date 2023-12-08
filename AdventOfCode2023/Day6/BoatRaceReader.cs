using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Runner;

internal class BoatRaceReader
{
    public List<BoatRace> Read(string[] lines)
    {
        var records = lines[0].SkipPastColon()
            .Where(char.IsDigit)
            .CollapseToString();

        var distances = lines[1].SkipPastColon()
            .Where(char.IsDigit)
            .CollapseToString();

        return new List<BoatRace>()
        {
            new()
            {
                DistanceRecord = long.Parse(distances),
                Time = long.Parse(records)
            }
        };

        return records.Zip(distances)
            .Select(x => new BoatRace()
            {
                Time = x.First,
                DistanceRecord = x.Second
            })
            .ToList();
    }
}

internal class BoatRace
{
    public long Time { get; set; }
    public long DistanceRecord { get; set; }
}