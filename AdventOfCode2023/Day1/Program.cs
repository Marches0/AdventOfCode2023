using Day1;

CalibrationReader reader = new();

var lines = File.ReadAllLines("day1_input.txt")
    .Select(l => new { Line = l, Value = reader.ReadLine(l) })
    .ToList();
    
foreach(var line in lines)
{
    Console.WriteLine($"{line.Line} = {line.Value}");
}

var sum = lines
    .Sum(l => l.Value);

Console.WriteLine(sum);