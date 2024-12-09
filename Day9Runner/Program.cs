using System.Diagnostics;

Console.Clear();
var input = await File.ReadAllTextAsync("evil.txt");
// 6287317016845
var sw = new Stopwatch();
sw.Start();
var result = Day9Lib.Solver.SolvePart2(input);
sw.Stop();
Console.WriteLine(sw.ElapsedMilliseconds);
Console.WriteLine(result);