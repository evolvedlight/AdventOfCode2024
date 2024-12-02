using Day2Lib;

var fileLines = await File.ReadAllLinesAsync("input.txt");

var numSafe = fileLines.Select(x => Part1.IsSafe(x.Split(' ').Select(x => int.Parse(x)))).Where(x => x == true).Count();

Console.WriteLine($"Number safe: {numSafe}");

var numSafe2 = fileLines.Select(x => Part2.IsSafeBruteForce(x.Split(' ').Select(x => int.Parse(x)).ToList())).Where(x => x == true).Count();

Console.WriteLine(numSafe2);