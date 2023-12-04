namespace Day4
{
    public class Solver
    {
        public void Run()
        {
            var reader = new ScratchCardReader();

            List<ScratchCard> scratchCards = File.ReadAllLines("day4_input.txt")
                .Select(l => reader.Read(l))
                .ToList();

            Part2(scratchCards);
        }

        private int GetMatchedNumberCount(ScratchCard card)
        {
            return card.WinningNumbers.Intersect(card.ScratchedNumbers).Count();
        }

        private int GetPoints(ScratchCard card)
        {
            var winningNumberCount = GetMatchedNumberCount(card);
            return winningNumberCount == 0
                ? 0
                : (int)Math.Pow(2, winningNumberCount - 1);
        }

        private void Part2(List<ScratchCard> scratchCards)
        {
            for (int i = 0; i < scratchCards.Count; i++)
            {
                // For each match you get, you get the next card along.
                // e.g. Card 1 with 2 matches gets you a card 2, and a card 3
                // This means that you now have two card 2s, each of which gives you more cards
                int matchingNumbers = GetMatchedNumberCount(scratchCards[i]);
                for(int j = 0; j < matchingNumbers; j++)
                {
                    scratchCards[i + j + 1].Copies += scratchCards[i].Copies;
                }
            }

            var total = scratchCards.Sum(s => s.Copies);
            Console.WriteLine(total);
        }

        private void Part1(List<ScratchCard> scratchCards)
        {
            var totalPoints = scratchCards
                .Select(GetPoints)
                .Sum();

            Console.WriteLine(totalPoints);
        }
    }
}