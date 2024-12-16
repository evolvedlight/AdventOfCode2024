Console.WriteLine("Start");
var gridLines = await File.ReadAllLinesAsync("example21.txt");

var arrayLines = gridLines.Select(line => line.ToCharArray()).ToArray();

var result = Day16Lib.GridSolver.FindShortestRoute(arrayLines);

Console.WriteLine(result);