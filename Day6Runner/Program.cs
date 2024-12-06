using Day6Lib;
using System.Diagnostics;

var input = await File.ReadAllLinesAsync("input.txt");

var map = new MapBoard(Day6Lib.MapParser.ParseMap(input.ToList()));
//map.Print();

var sw = new Stopwatch();
sw.Start();
var res = Solver.Part2GetGuardStuck(map);
sw.Stop();
Console.WriteLine($"{res} in {sw.ElapsedMilliseconds}ms");

var sw2 = new Stopwatch();
sw2.Start();
var res2 = Solver.Part2GetGuardStuckOptimised(map);
sw2.Stop();
Console.WriteLine($"{res2} in {sw2.ElapsedMilliseconds}ms");