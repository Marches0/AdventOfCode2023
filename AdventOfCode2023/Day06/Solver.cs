using Runner;

namespace Day6
{
    public class Solver
    {
        public void Run()
        {
            var boatRaces = new BoatRaceReader().Read(File.ReadAllLines("day6_input.txt"));

            var wins = boatRaces
                .Select(WaysToWinCount)
                .ToList();

            var agg = wins
                .Skip(1)
                .Aggregate(wins.First(), (current, next) => current * next);

            Console.WriteLine(agg);
        }

        internal long WaysToWinCount(BoatRace boatRace)
        {
            // For each second you wait, you gain 1mm per ms of speed.
            // To win, we have to beat the current record of the race.
            // Time taken is bell curve-ish based on how long we wait for
            // so just find the upper and lower extremeties.
            long lowestTime = 0;
            for (int i = 1; ; i++)
            {
                if (BeatsRecord(i))
                {   
                    lowestTime = i;
                    break;
                }
            }

            long highestTime = 0;
            for(long i = boatRace.Time; ; i--)
            {
                if (BeatsRecord(i))
                {
                    highestTime = i;
                    break;
                }
            }

            return (highestTime - lowestTime) + 1;

            bool BeatsRecord(long waitTime)
            {
                double minTravelTime = boatRace.DistanceRecord / (float)waitTime;
                return waitTime + minTravelTime < boatRace.Time;
            }
        }
    }
}
