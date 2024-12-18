Console.WriteLine("Start");
var gridLines = await File.ReadAllLinesAsync("example5.txt");

var arrayLines = gridLines.Select(line => line.ToCharArray()).ToArray();

var (badc, badLines, badDistances) = Day16Lib.GridSolver.FindShortestRoute(arrayLines);
var (goodc, goodLines, goodDistances) = Day16Lib.GridSolver.FindBestPathCountSquares(arrayLines);

foreach (var (badKey, badDistance) in badDistances)
{
    var goodDistance = goodDistances.GetValueOrDefault(badKey, -1);
    if (goodDistance != int.MaxValue && badDistance != int.MaxValue)
    {
        Console.WriteLine($"{badKey}: {badDistance} | {goodDistance}");
    }
   
}

Console.WriteLine(badc);
Console.WriteLine(goodc);