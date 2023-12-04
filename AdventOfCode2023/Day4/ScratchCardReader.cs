using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day4
{
    internal class ScratchCardReader
    {
        public ScratchCard Read(string line)
        {
            //         Winning numbers| Scratched numbers 
            // Card 1: 41 48 83 86 17 | 83 86  6 31 17  9 48 53
            string numbers = line.SkipWhile(c => c != ':')
                .Skip(1)
                .CollapseToString();

            var numberGroups = numbers
                .Split('|')
                .Select(g => g.Split(' ', StringSplitOptions.RemoveEmptyEntries))
                .ToList();

            return new ScratchCard()
            {
                WinningNumbers = numberGroups[0].Select(int.Parse).ToList(),
                ScratchedNumbers = numberGroups[1].Select(int.Parse).ToList(),
            };
        }
    }

    internal class ScratchCard
    {
        public List<int> WinningNumbers { get; set; }
        public List<int> ScratchedNumbers { get; set; }
    }
}
