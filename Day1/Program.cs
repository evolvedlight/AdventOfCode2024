var fileLines = await File.ReadAllLinesAsync("input.txt");

List<int> lineA = [];
List<int> lineB = [];

foreach (var line in fileLines)
{
    var parts = line.Split(' ');
    lineA.Add(int.Parse(parts[0]));
    lineB.Add(int.Parse(parts.Last()));
}

lineA.Sort();
lineB.Sort();
var result = Enumerable.Zip(lineA, lineB).Select((numbers) => Math.Abs(numbers.First - numbers.Second)).Sum();

Console.WriteLine(result);