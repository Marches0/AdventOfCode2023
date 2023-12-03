namespace Day3
{
    public class Solver
    {
        public void Run()
        {
            var file = File.ReadAllLines("day3_input.txt");
            Schematic schematic = new SchematicReader().Read(file);
            var sum = GetPartNumbers(schematic)
                .Sum();

            Console.WriteLine(sum);
        }

        private List<int> GetPartNumbers(Schematic schematic)
        {
            List<int> partNumbers = new();

            // A number is a part number if any of its digits
            // in the schematic are next to a symbol. Includes diagonals.
            var height = schematic.Items.GetLength(0);
            var width = schematic.Items.GetLength(1);
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    IEngineItem current = schematic.Items[i, j];
                    if (current is not Number number || number.IsPartNumber)
                    {
                        // IsPartNumber = true are already checked - don't need to repeat
                        continue;
                    }

                    if (NextToSymbol(i, j))
                    {
                        // Bit hacky, but I did this to myself by using
                        // multidimensional arrays
                        number.IsPartNumber = true;
                        partNumbers.Add(number.Value);
                    }
                }
            }

            return partNumbers;

            bool NextToSymbol(int y, int x)
            {
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
                        if (schematic.Items[i, j] is Symbol)
                        {
                            return true;
                        }
                    }
                }

                return false;
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
