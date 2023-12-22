namespace Day14;

public class Solver
{
    public void Run()
    {
        var dish = new DishReader().Read(File.ReadAllLines("day14_input.txt"));
        SlideNorth(dish);
        ShowDish(dish);

        var load = CalculateLoad(dish);
        Console.WriteLine(load);
    }

    private void SlideNorth(Dish dish)
    {
        for (int i = 1; i < dish.Content.Count; i++)
        {
            for (int j = 0; j < dish.Content[i].Count; j++)
            {
                // Check up one at a time to find the final resting
                // place
                // Since we go from top to bottom, we don't have to do a reprocess
                // after doing it once, since the rocks higher than other ones that
                // can move will already have been moved.
                if (dish.Content[i][j] != DishContent.RoundRock)
                {
                    continue;
                }

                // The rock's pos will be set later. If it doesn't move,
                // that is fine.
                dish.Content[i][j] = DishContent.Empty;

                int finalYPos = i;

                for (int k = i; k >= 0; k--)
                {
                    if (CanSlideInto(dish.Content[k][j]))
                    {
                        finalYPos = k;
                    }
                    else
                    {
                        break;
                    }
                }

                dish.Content[finalYPos][j] = DishContent.RoundRock;
            }
        }
    }

    private int CalculateLoad(Dish dish)
    {
        int maxRockLoad = dish.Content.Count();
        int load = 0;

        for (int i = 0; i < dish.Content.Count; i++)
        {
            var rounded = dish.Content[i].Count(c => c == DishContent.RoundRock);
            var loadHere = maxRockLoad - i;
            load += rounded * loadHere;
        }

        return load;
    }

    private bool CanSlideInto(DishContent dish)
    {
        return dish == DishContent.Empty;
    }

    private void ShowDish(Dish dish)
    {
        foreach(var item in dish.Content)
        {
            Console.WriteLine(string.Join("", item.Select(GetDisplay)));
        }
    }

    private string GetDisplay(DishContent dishContent)
    {
        return dishContent switch
        {
            DishContent.Empty => ".",
            DishContent.RoundRock => "O",
            DishContent.CubeRock => "#",
            _ => throw new NotImplementedException()
        };
    }
}