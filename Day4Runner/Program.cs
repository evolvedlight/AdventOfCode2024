var inputLines = await File.ReadAllLinesAsync("input.txt");

var inputArray = inputLines.Select(x => x.ToCharArray()).ToArray();

//var result = Day4Lib.Part1.GetXmases(inputArray);
var result = Day4Lib.Part2.GetMasXes(inputArray);
Console.WriteLine(result);