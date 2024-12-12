using Day12Lib;

var lines = await File.ReadAllLinesAsync("input.txt");

var garden = Garden.FromLines(lines);

var regions = garden.GetRegions();

var sum = 0;
foreach (var region in regions)
{
    Console.WriteLine($"{region.PlantType}: {region.Area} * Sides: {region.Sides}");
    sum += region.Area * region.Sides;

    for (int row = 0; row < 5; row++)
    {
        for (int column = 0; column < 5; column++)
        {
            var cand = region.Plots.Where(p => p.row == row && p.col == column);
            if (cand.Any())
            {
                Console.Write(cand.First().corners);
            }
            else
            {
                Console.Write(".");
            }
        }
        Console.WriteLine();
    }
}

Console.WriteLine(sum);