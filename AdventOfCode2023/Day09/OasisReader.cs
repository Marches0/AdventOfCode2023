using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day9;

internal class OasisReader
{
    public List<long> GetReading(string line)
    {
        return line.GetNumbers(" ");
    }
}