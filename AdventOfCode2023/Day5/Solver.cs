using System.Collections.Concurrent;

namespace Day5
{
    public class Solver
    {
        public void Run()
        {
            var almanac = new AlmanacReader()
                .Read(File.ReadAllLines("day5_input.txt"));

            // 125742456
            // 125742457 wrong - we're off by 1???
            ConcurrentBag<long> results = new ConcurrentBag<long>();

            long startLoc = 125742457;
            long seedLoc = almanac.GetSeed(startLoc);
            long endLoc = almanac.GetLocation(seedLoc);


            long goodstartLoc = 125742456;

            var res = Parallel.ForEach(LocationValues(), (l, state) =>
            {
                var seed = almanac.GetSeed(l);
                if (almanac.ContainsSeed(seed))
                {
                    results.Add(l);
                    Console.WriteLine("Seed " + seed + " Location " + l);
                }

                if(results.Count > 200)
                {
                    state.Break();
                }
            });

            do
            {
                Thread.Sleep(200);
            }while(res.IsCompleted);

            var lowest = results.Min();
            Console.WriteLine(results.Min());

            if(lowest == 125742457)
            {
                Console.WriteLine("wrong");
            }
        }

        private IEnumerable<long> LocationValues()
        {
            long current = 0;
            for (;;)
            {
                yield return Interlocked.Increment(ref current);
            }
        }

        private void Write(Almanac almanac)
        {
            List<string> lines = new List<string>();
        }
    }
}