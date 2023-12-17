namespace Day8
{
    public class Solver
    {
        public void Run()
        {
            var desertMap = new DesertMapReader().Read(File.ReadAllLines("day8_input.txt"));
            var distance = Part2(desertMap);
            Console.WriteLine(distance);
        }

        private long Part1(DesertMap map, string start, string end)
        {
            using var directionSource = GetDirections(map).GetEnumerator();
            directionSource.MoveNext();

            DesertTile current = map.Tiles[start];
            long travelled = 0;
            
            do
            {
                current = current.Travel(directionSource.Current);

                ++travelled;
                directionSource.MoveNext();

            } while (current.Name != end);

            return travelled;
        }

        private long Part2(DesertMap map)
        {
            // Start on all nodes ending with A at the same time.
            // Advance each one forward at the same time.
            // Stop when all are on nodes ending with Z at the same time.
            List<DesertTile> starts = map.Tiles
                .Where(t => t.Key.EndsWith("A"))
                .Select(t => t.Value)
                .ToList();

            // Get the distance it takes to reach a Z, and then
            // find the lcm of all of those
            var allTravels = starts
                .Select(s => Part2Travel(map, s))
                .ToList();

            var lcm = LowestCommonMultiple(allTravels);

            return lcm;
        }

        private long Part2Travel(DesertMap map, DesertTile start)
        {
            using var directionSource = GetDirections(map).GetEnumerator();
            directionSource.MoveNext();
            long travelled = 0;
            DesertTile current = start;

            do
            {
                current = current.Travel(directionSource.Current);

                ++travelled;
                directionSource.MoveNext();
            } while (!current.Name.EndsWith("Z"));

            return travelled;
        }

        private IEnumerable<Direction> GetDirections(DesertMap map)
        {
            for(;;)
            {
                foreach (var direction in map.Directions)
                {
                    yield return direction;
                }
            }
        }

        private long LowestCommonMultiple(List<long> numbers)
        {
            var lcm = numbers.First();

            foreach(var number in numbers.Skip(1))
            {
                lcm = lcm_calc(lcm, number);
            }

            return lcm;
        }

        // https://stackoverflow.com/questions/13569810/least-common-multiple
        static long gcf(long a, long b)
        {
            while (b != 0)
            {
                long temp = b;
                b = a % b;
                a = temp;
            }
            return a;
        }

        static long lcm_calc(long a, long b)
        {
            return (a / gcf(a, b)) * b;
        }
    }
}
