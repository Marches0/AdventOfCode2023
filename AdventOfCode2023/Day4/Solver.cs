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

            var totalPoints = scratchCards
                .Select(GetPoints)
                .Sum();

            Console.WriteLine(totalPoints);
        }

        private int GetPoints(ScratchCard card)
        {
            var winningNumberCount = card.WinningNumbers.Intersect(card.ScratchedNumbers).Count();
            return winningNumberCount == 0
                ? 0
                : (int)Math.Pow(2, winningNumberCount - 1);
        }
    }
}