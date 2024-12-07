using Day7Lib;
using System.Diagnostics;
Console.WriteLine("Starting");
var fileLines = await File.ReadAllLinesAsync("input.txt");

var sw = new Stopwatch();
sw.Start();
var res = Solver.GetPossibleWaysSum2(fileLines);
sw.Stop();
Console.WriteLine(res);
Console.WriteLine(sw.ElapsedMilliseconds);