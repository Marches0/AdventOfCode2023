namespace Day1
{
    internal class CalibrationReader
    {
        private readonly new List<string[]> _numbers = new()
        {
            new[]{"1", "one"},
            new[]{"2", "two"},
            new[]{"3", "three"},
            new[]{"4", "four"},
            new[]{"5", "five"},
            new[]{"6", "six"},
            new[]{"7", "seven"},
            new[]{"8", "eight"},
            new[]{"9", "nine"},
        };

        public int ReadLine(string line)
        {
            // Value = first & last digit combined
            // One digit means that one is both first and last

            // The string may contain the words for digits too,
            // e.g. "one" instead of one.
            // We can't replace because we may break "fused" numbers
            // e.g. "twone" should be 21

            // Create a sliding window and check to see when we start
            // with the first digit - "zzone" -> "zzone", "zone", "one".
            string? firstDigit = null;
            for(int i = 0; i < line.Length; i++)
            {
                string testString = new string(line.Skip(i).ToArray());
                string[]? firstNumber = _numbers.FirstOrDefault(n => n.Any(x => testString.StartsWith(x)));

                if (firstNumber != null)
                {
                    firstDigit = firstNumber[0];
                    break;
                }
            }

            if (firstDigit is null)
            {
                throw new InvalidOperationException("Missing first digit.");
            }

            // Create a sliding window from the back
            // "zzone" -> "e", "ne", "one"
            string? lastDigit = null;
            for (int i = 1; i < line.Length + 1; i++) // Go Length + 1 because we are taking (1-based), not indexing (0-based)
            {
                string testString = new string(line.TakeLast(i).ToArray());
                string[]? lastNumber = _numbers.FirstOrDefault(n => n.Any(x => testString.StartsWith(x)));

                if (lastNumber != null)
                {
                    lastDigit = lastNumber[0];
                    break;
                }
            }

            if (lastDigit is null)
            {
                throw new InvalidOperationException("Missing last digit.");
            }

            return int.Parse(
                firstDigit
              + lastDigit
            );
        }
    }
}