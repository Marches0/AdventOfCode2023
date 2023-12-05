using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day5;

internal class AlmanacReader
{
    public Almanac Read(string[] lines)
    {
        // seeds: 79 14 55 13
        var seeds = lines[0].SkipWhile(c => c != ':')
            .Skip(1)
            .CollapseToString()
            .GetNumbers(" ");

        // The maps are in the order we'd want to traverse them, so just add them in that order
        // and don't worry about the details
        return new Almanac()
        {
            Seeds = seeds,
            Maps = ReadMaps(lines.Skip(2)) // Skip the seeds + empty line after it
        };
    }

    private List<MapCollection> ReadMaps(IEnumerable<string> lines)
    {
        /*
         * seed-to-soil map:
            50 98 2
            52 50 48

            soil-to-fertilizer map:
            0 15 37
            37 52 2
            39 0 15
         * 
         */
        MapCollection currentMap = new MapCollection();
        List<MapCollection> maps = new List<MapCollection>()
        {
            currentMap
        };

        using IEnumerator<string> enumerator = lines.GetEnumerator();
        enumerator.MoveNext();
        currentMap.Name = enumerator.Current;

        while (enumerator.MoveNext())
        {
            var line = enumerator.Current;

            if (line == "")
            {
                // Next map collection starting
                currentMap = new();
                maps.Add(currentMap);

                if (enumerator.MoveNext())
                {
                    currentMap.Name = enumerator.Current;
                }
                else
                {
                    // It's all ogre now
                    return maps;
                }
            }
            else
            {
                var numbers = line.GetNumbers(" ");
                currentMap.Maps.Add(new Map()
                {
                    DestinationStart = numbers[0],
                    SourceStart = numbers[1],
                    Range = numbers[2]
                });
            }
        }

        return maps;
    }
}

internal class Almanac
{
    public List<long> Seeds { get; set; }
    public List<MapCollection> Maps { get; set; }


    public long GetLocation(long seedNumber)
    {
        return Maps
            .Aggregate(seedNumber, (current, map) => map.MapSource(current));
    }
}

internal class MapCollection
{
    public string Name { get; set; }
    public List<Map> Maps { get; set; } = new List<Map>();

    public long MapSource(long source)
    {
        // If it doesn't match to a range, it uses itself.
        return Maps
            .Select(m => m.MapSource(source))
            .Where(s => s != null)
            .FirstOrDefault() ?? source;
    }
}

internal class Map
{
    public long SourceStart { get; set; }
    public long DestinationStart { get; set; }
    public long Range { get; set; }

    private long MaxSource => SourceStart + Range;

    public long? MapSource(long source)
    {
        if (source < SourceStart || source > MaxSource)
        {
            return null;
        }

        var distance = source - SourceStart;
        return DestinationStart + distance;
    }
}
