using Runner;

namespace Day7
{
    public class Solver
    {
        public void Run()
        {
            var reader = new CamelCardReader();
            var hands = File.ReadAllLines("day7_input.txt")
                .Select(reader.Read)
                .ToList();

            var classifier = new HandTypeClassifier();
            hands.ForEach(h => h.HandType = classifier.GetHandType(h.Cards));

            var order = hands
                .OrderBy(h => h, new HandComparer())
                .ToList();

            var winnings = order
                .Select((h, i) => h.Bid * (i + 1))
                .Sum();

            Console.WriteLine(winnings);
        }
    }

    public class HandComparer : IComparer<Hand>
    {
        int IComparer<Hand>.Compare(Hand x, Hand y)
        {
            // <0 = x less
            //  0 = x equal
            // >0 = x more
            if (x.HandType > y.HandType)
            {
                return 1;
            }

            if (x.HandType < y.HandType)
            {
                return -1;
            }

            var decider = x.Cards
                .Zip(y.Cards)
                .FirstOrDefault(x => x.First != x.Second);

            if (decider is (0, 0))
            {
                return 0;
            }

            if (decider.First > decider.Second)
            {
                return 1;
            }

            if (decider.First < decider.Second)
            {
                return -1;
            }

            return 0;
        }
    }
}