using Day10Lib;

var inputLines = await File.ReadAllLinesAsync("input.txt");

var map = Day10Lib.Parser.ParseMap(inputLines);
var solver = new Solver(map);
var result = solver.SolvePart2();

Console.WriteLine(result);