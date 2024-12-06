// See https://aka.ms/new-console-template for more information
using Day5Lib;

Console.WriteLine("Hello, World!");

var input = await File.ReadAllLinesAsync("input.txt");

var parsed = Parser.Parse(input.ToList());
var result = Solver.SolveP2Comparer(parsed);
Console.WriteLine(result);