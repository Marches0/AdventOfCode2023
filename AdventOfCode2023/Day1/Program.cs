using Day1;

CalibrationReader reader = new();

int sum = File.ReadAllLines("day1_input.txt")
    .Select(reader.ReadLine)
    .Sum();

Console.WriteLine(sum);