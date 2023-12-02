using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared;

namespace Day2
{
    internal class CubeGameReader
    {
        public Game Read(string line)
        {
            // Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green
            char[] gameIdDigits = line
                .TakeWhile(c => c != ':')
                .Where(char.IsDigit)
                .ToArray();

            int gameId = int.Parse(new string(gameIdDigits));

            // Remove the game id section from the start
            // to simplify later parsing
            var draws = line
                .Skip("Game:  ".Length + gameIdDigits.Length)
                .CollapseToString()
                .Split(';')
                .Select(ReadDraw)
                .ToList();

            return new Game()
            {
                Id = int.Parse(new string(gameIdDigits)),
                Draws = draws
            };
        }

        private CubeDraw ReadDraw(string set)
        {
            // 3 blue, 4 red
            // 1 red, 2 green, 6 blue
            // 2 green
            var draws = set.Split(',')
                .Select(d => d.Trim())
                .Select(d => d.Split(' '))
                .Select(d => new CubeGroup(int.Parse(d[0]), d[1]))
                .ToList();

            return new CubeDraw()
            {
                Groups = draws
            };
        }
    }

    internal class Game
    {
        public int Id { get; set; }
        public List<CubeDraw> Draws { get; set; }
    }

    internal class CubeDraw
    {
        public List<CubeGroup> Groups { get; set; }
    }

    internal class CubeGroup
    {
        public CubeGroup(int count, string colour)
        {
            Count = count;
            Colour = colour;
        }

        public int Count { get; }
        public string Colour { get; }
    }
}
