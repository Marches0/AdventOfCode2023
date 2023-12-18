namespace Day12;

public class Solver
{
    public void Run()
    {
        var reader = new HotSpringReader();
        var springs = File.ReadAllLines("day12_input.txt")
            .Select(reader.Read)
            .ToList();

        var solvers = springs
            .Select(s => 
            {
                var solver = new HotSpringSolver(s);
                solver.Solve();
                //Console.WriteLine($"{s} has {solver.Arrangements.Count}");
                return solver;
            })
            .ToList();

        Console.WriteLine(solvers.Sum(s => s.Arrangements.Count));
    }    
}