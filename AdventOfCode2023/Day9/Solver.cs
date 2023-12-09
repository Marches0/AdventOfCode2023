namespace Day9
{
    public class Solver
    {
        public void Run()
        {
            var reader = new OasisReader();
            var readings = File.ReadAllLines("day9_input.txt")
                .Select(reader.GetReading)
                .ToList();

            var values = readings
                .Select(GetStepValue)
                .ToList();

            var total = values.Sum();
            Console.WriteLine(total);
        }

        private long GetStepValue(List<long> reading)
        {
            List<List<long>> sequences = new List<List<long>>()
            {
                reading
            };

            List<long> current = reading;
            do
            {
                current = GetDifferenceReading(current);
                sequences.Add(current);
            } while (!IsFinalStep(current));

            return GetNextValueSum(sequences);
        }

        private List<long> GetDifferenceReading(List<long> reading)
        {
            return reading.Zip(reading.Skip(1))
                .Select(r => r.Second - r.First)
                .ToList();
        }

        private bool IsFinalStep(List<long> reading)
        {
            return reading.All(r => r == 0);
        }

        private long GetNextValueSum(List<List<long>> sequences)
        {
            for (int i = sequences.Count - 1; i > 0; i--)
            {
                var startVal = sequences[i].Last();
                var nextSequence = sequences[i - 1];
                nextSequence.Add(startVal + nextSequence.Last());
            }

            return sequences
                .First()
                .Last();
        }
    }
}
