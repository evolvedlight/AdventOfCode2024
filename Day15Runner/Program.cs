using Day15Lib;
using static Day15Lib.Grid;

var gridLines = await File.ReadAllLinesAsync("testcorner.txt");

var grid = Parser.ParseRobotInstructionsFat(gridLines);
while (grid.HasInstructions)
{
    grid.FollowInstruction();
    grid.PrintGrid();
    await Task.Delay(150);
}


Console.WriteLine(grid.GetGpsResult());
