using Day15Lib;
using static Day15Lib.Grid;

var gridLines = await File.ReadAllLinesAsync("input.txt");

var grid = Parser.ParseRobotInstructionsFat(gridLines);
while (grid.HasInstructions)
{
    grid.FollowInstruction();
    grid.PrintGrid();
    await Task.Delay(5);
}


Console.WriteLine(grid.GetGpsResult());
