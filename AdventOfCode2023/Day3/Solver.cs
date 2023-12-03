namespace Day3
{
    public class Solver
    {
        public void Run()
        {
            var file = File.ReadAllLines("day3_input.txt");
            Schematic schematic = new SchematicReader().Read(file);
            SetNeighbours(schematic);

            var gearRatios = schematic.Items
                .Where(i => i is Symbol s && s.Neighbours.Count == 2)
                .Cast<Symbol>()
                .Select(s => s.Neighbours[0].Value * s.Neighbours[1].Value);

            int sum = gearRatios.Sum();

            Console.WriteLine(sum);
        }

        private List<int> SetNeighbours(Schematic schematic)
        {
            List<int> partNumbers = new();

            // A number is a part number if any of its digits
            // in the schematic are next to a symbol. Includes diagonals.
            var height = schematic.ItemGrid.GetLength(0);
            var width = schematic.ItemGrid.GetLength(1);
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    IEngineItem current = schematic.ItemGrid[i, j];
                    if (current is not Symbol symbol)
                    {
                        continue;
                    }

                    symbol.Neighbours = NeighbouringNumbers(i, j);
                }
            }

            return partNumbers;

            List<Number> NeighbouringNumbers(int y, int x)
            {
                List<Number> neighbours = new List<Number>();
                // Ranges to check. Inclusive.
                int minY = Clamp(y - 1, height);
                int maxY = Clamp(y + 1, height);
                int minX = Clamp(x - 1, width);
                int maxX = Clamp(x + 1, width);
                // Clamp so we don't go out of the index's bounds
                for (int i = minY; i <= maxY; i++)
                { 
                    for (int j = minX; j <= maxX; j++)
                    {
                        if (schematic.ItemGrid[i, j] is Number n)
                        {
                            neighbours.Add(n);
                        }
                    }
                }

                return neighbours.Distinct().ToList();
            }

            static int Clamp(int val, int max)
            {
                if (val < 0)
                {
                    return 0;
                }

                if (val >= max)
                {
                    return max - 1;
                }

                return val;
            }
        }
    }
}
