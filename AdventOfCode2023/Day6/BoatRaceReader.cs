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
            .GetNumbers(" ");

        var distances = lines[1].SkipPastColon()
            .GetNumbers(" ");

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