using Day2Lib;

var fileLines = await File.ReadAllLinesAsync("input.txt");

var numSafe = fileLines.Select(x => Part1.IsSafe(x.Split(' ').Select(x => int.Parse(x)))).Where(x => x == true).Count();

Console.WriteLine($"Number safe: {numSafe}");

var safe2 = fileLines.Select(x => Part2.IsSafe(x.Split(' ').Select(x => int.Parse(x)).ToList())).ToList();
var safe2BruteForce = fileLines.Select(x => Part2.IsSafeBruteForce(x.Split(' ').Select(x => int.Parse(x)).ToList())).ToList();


for (int i = 0; i < numSafe; i++)
{
    if (safe2[i] != safe2BruteForce[i])
    {
        var line = fileLines[i];
        Console.WriteLine($"{line}: {safe2[i]} | {safe2BruteForce[i]}");
    }
    
}

Console.WriteLine(safe2.Where(x => x == true).Count());
Console.WriteLine(safe2BruteForce.Where(x => x == true).Count());