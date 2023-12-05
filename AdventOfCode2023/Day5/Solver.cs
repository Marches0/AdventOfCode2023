namespace Day5
{
    public class Solver
    {
        public void Run()
        {
            var almanac = new AlmanacReader()
                .Read(File.ReadAllLines("day5_input.txt"));

            var location = almanac.Seeds
                .Select(almanac.GetLocation)
                .Min();

            Console.WriteLine(location);
        }
    }
}