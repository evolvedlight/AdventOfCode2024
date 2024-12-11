var input = await File.ReadAllTextAsync("input.txt");

var stoneLines = new Day11Lib.StoneLinesP2(input.Split(" ").Select(x => long.Parse(x)).ToList());

Console.WriteLine(stoneLines.Blink75());