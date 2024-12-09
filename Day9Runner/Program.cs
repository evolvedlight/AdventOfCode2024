Console.Clear();
var input = await File.ReadAllTextAsync("example.txt");

var result = Day9Lib.Solver.SolvePart1(input);

Console.WriteLine(result);