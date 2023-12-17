namespace Day2
{
    public class Solver
    {
        public void Run()
        {
            var reader = new CubeGameReader();
            var games = File.ReadAllLines("day2_input.txt")
                .Select(reader.Read)
                .ToList();

            //Part1(games);
            Part2(games);
        }

        private void Part1(List<Game> games)
        {
            var sum = games
                .Where(g => PossibleToContain(g, new CubeGroup(12, "red")))
                .Where(g => PossibleToContain(g, new CubeGroup(13, "green")))
                .Where(g => PossibleToContain(g, new CubeGroup(14, "blue")))
                .Sum(g => g.Id);

            Console.WriteLine(sum);
        }

        private void Part2(List<Game> games)
        {
            var totalSetPower = games.Select(GetMinimalGroups)
                .Select(SetPower)
                .Sum();

            Console.WriteLine(totalSetPower);
        }

        /// <summary>
        ///  Whether or not a given <see cref="CubeGroup"/> could have been present in <paramref name="game"/>.
        /// </summary>
        private bool PossibleToContain(Game game, CubeGroup group)
        {
            IEnumerable<CubeGroup> sameColour = game.Draws
                .SelectMany(d => d.Groups)
                .Where(s => s.Colour == group.Colour);

            return sameColour.All(c => c.Count <= group.Count);
        }

        /// <summary>
        ///  The smallest cube groups that could have been used to create <paramref name="game"/>.
        /// </summary>
        /// <returns></returns>
        private List<CubeGroup> GetMinimalGroups(Game game)
        {
            return game.Draws
                .SelectMany(d => d.Groups)
                .GroupBy(g => g.Colour)
                .Select(x => x.MaxBy(g => g.Count))
                .ToList();
        }

        private int SetPower(List<CubeGroup> cubeGroups)
        {
            return cubeGroups
                .Skip(1)
                .Aggregate(cubeGroups.First().Count, (total, next) => total * next.Count);
        }
    }
}
