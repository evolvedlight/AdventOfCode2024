var fileLines = await File.ReadAllLinesAsync("input.txt");

List<int> lineA = [];
List<int> lineB = [];

foreach (var line in fileLines)
{
    var parts = line.Split(' ');
    lineA.Add(int.Parse(parts[0]));
    lineB.Add(int.Parse(parts.Last()));
}

// Part 1
lineA.Sort();
lineB.Sort();
var result = Enumerable.Zip(lineA, lineB).Select((numbers) => Math.Abs(numbers.First - numbers.Second)).Sum();

Console.WriteLine($"Part 1 result: {result}");

// Part 2
var lineBCounts = lineB.CountBy(x => x).ToDictionary();
var resultDay2 = lineA.Select(x => x * lineBCounts.GetValueOrDefault(x, 0)).Sum();

Console.WriteLine($"Part 2 result: {resultDay2}");