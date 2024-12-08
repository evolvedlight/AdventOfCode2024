var fileLines = await File.ReadAllLinesAsync("input.txt");

var grid = fileLines.Select(x => x.ToCharArray()).ToArray();

Console.Clear();
var numberAntiNodes = Day8Lib.Part1Solver.GetNumberOfAntiNodesWithVisualisation(grid);

Console.WriteLine(numberAntiNodes);