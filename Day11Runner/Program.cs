using System.Diagnostics;

var input = await File.ReadAllTextAsync("input.txt");

var stoneLines = new Day11Lib.StoneLinesP2(input.Split(" ").Select(x => long.Parse(x)).ToList());

var sw = new Stopwatch();
sw.Start();
Console.WriteLine(stoneLines.Blink75());
sw.Stop();

var sw2 = new Stopwatch();
sw2.Start();
var stoneLines2 = new Day11Lib.SolverBuckets(input.Split(" ").Select(x => long.Parse(x)).ToList());
sw2.Stop();
Console.WriteLine(stoneLines2.Blink75());

Console.WriteLine($"SW time: {sw.ElapsedMilliseconds}. SW2 time: {sw2.ElapsedMilliseconds}");