namespace Day2
{
    public class Solver
    {
        public void Run()
        {
            var reader = new CubeGameReader();
            List<Game> games = File.ReadAllLines("day2_input.txt")
                .Select(reader.Read)
                .Where(g => PossibleToContain(g, new CubeGroup(12, "red")))
                .Where(g => PossibleToContain(g, new CubeGroup(13, "green")))
                .Where(g => PossibleToContain(g, new CubeGroup(14, "blue")))
                .ToList();

            var sum = games
                .Sum(g => g.Id);

            Console.WriteLine(sum);
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
    }
}
