using System.Text.RegularExpressions;

var exampleInput = await File.ReadAllTextAsync("example2.txt");
var actualInput = await File.ReadAllTextAsync("input.txt");

var solver = new Day3Lib.Part2();

Console.WriteLine(solver.SolveWithBigRegex(actualInput));
Console.WriteLine(solver.Solve(actualInput));