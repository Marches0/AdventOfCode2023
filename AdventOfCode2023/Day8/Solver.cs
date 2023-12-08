namespace Day8
{
    public class Solver
    {
        public void Run()
        {
            var desertMap = new DesertMapReader().Read(File.ReadAllLines("day8_input.txt"));
            var distance = GetDistance(desertMap, "AAA", "ZZZ");
            Console.WriteLine(distance);
        }

        private long GetDistance(DesertMap map, string start, string end)
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

        private IEnumerable<Direction> GetDirections(DesertMap map)
        {
            for(;;)
            {
                foreach(var direction in map.Directions)
                {
                    yield return direction;
                }
            }
        }
    }
}
