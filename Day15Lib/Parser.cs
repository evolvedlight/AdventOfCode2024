

using System.Text;
using Visualisation;

namespace Day15Lib;

public class Grid
{
    readonly char[][] grid;
    int robotRow;
    int robotCol;
    readonly Queue<char> queueOfInstructions;  

    public Grid(char[][] startPositions, char[] instructions)
    {
        grid = startPositions;
        queueOfInstructions = new Queue<char>(instructions);

        for (var row = 0; row < grid.Length; row++)
        {
            for (var col = 0; col < grid[row].Length; col++)
            {
                if (grid[row][col] == '@')
                {
                    grid[row][col] = '.';
                    robotRow = row;
                    robotCol = col;
                }
            }
        }
    }

    public bool HasInstructions => queueOfInstructions.Count > 0;

    public void FollowInstruction()
    {
        var nextInstruction = queueOfInstructions.Dequeue();

        Dictionary<char, (int row, int col)> directionMapping = new()
        {
            { '<', (0, -1) },
            { '>', (0, 1) },
            { '^', (-1, 0) },
            { 'v', (1, 0) }
        };
        var direction = directionMapping[nextInstruction];

        var targetSquareRow = robotRow + direction.row;
        var targetSquareCol = robotCol + direction.col;
        var nextSquare = grid[targetSquareRow][targetSquareCol];

        // if empty, move in
        if (nextSquare == '.')
        {
            robotRow = targetSquareRow;
            robotCol = targetSquareCol;
            return;
        }

        if (nextSquare == '#')
        {
            // do nothing if we hit the wall
        }

        if (nextSquare == 'O')
        {
            if (nextInstruction == '>')
            {

            }
            // we want to find all boxes and then check there's an empty space after to push them
            var nextSquareAfter = grid[robotRow + (direction.row * 2)][robotCol + (direction.col * 2)];
            int countBoxes = 1;
            while (nextSquareAfter == 'O')
            {
                countBoxes++;
                nextSquareAfter = grid[robotRow + (direction.row * (countBoxes + 1))][robotCol + (direction.col * (countBoxes + 1))];
                
            }

            // if there's an empty space to move them into
            if (nextSquareAfter == '.')
            {
                // we push the boxes along and move the robot
                grid[robotRow + (direction.row * (countBoxes + 1))][robotCol + (direction.col * (countBoxes + 1))] = 'O';
                grid[targetSquareRow][targetSquareCol] = '.';
                robotRow = targetSquareRow;
                robotCol = targetSquareCol;
            }
        }

        if (nextSquare == '[' || nextSquare == ']')
        {
            var seen = new HashSet<(int, int)>();
            var boxesToPush = GetBoxesRecursivelyFromSquareAndDirection(direction, targetSquareRow, targetSquareCol, seen).ToList();
            var loopup = boxesToPush.ToDictionary(x => x, x => grid[x.row][x.column]);
            if (CanPush(boxesToPush, direction))
            {
                // remove boxes from grid
                for (var row = 0; row < grid.Length; row++)
                {
                    for (var col = 0; col < grid[row].Length; col++)
                    {
                        if (boxesToPush.Contains((row, col)))
                        {
                            grid[row][col] = '.';
                        }
                    }
                }

                // put in new place
                for (var row = 0; row < grid.Length; row++)
                {
                    for (var col = 0; col < grid[row].Length; col++)
                    {
                        if (boxesToPush.Contains((row, col)))
                        {
                            grid[row + direction.row][col + direction.col] = loopup[(row,col)];
                        }
                    }
                }

                robotRow = targetSquareRow;
                robotCol = targetSquareCol;
            }
        }
    }

    private bool CanPush(List<(int row, int column)> boxesToPush, (int row, int col) direction)
    {
        foreach (var (row, column) in boxesToPush)
        {
            var nextRow = row + direction.row;
            var nextColumn = column + direction.col;
            var nextSquare = grid[nextRow][nextColumn];

            if (nextSquare == '#')
            {
                return false;
            }
            
        }
        return true;
    }

    private List<(int row, int column)> GetBoxesRecursivelyFromSquareAndDirection((int row, int col) direction, int row, int col, HashSet<(int, int)> seen)
    {
        if (seen.Contains((row, col)))
        {
            return [];
        }
        seen.Add((row, col));
        var result = new List<(int row, int column)>();
        var square = grid[row][col];
        // if current space is a wall or empty space, return nothing
        if (square == '#' || square == '.')
        {
            return [];
        }

        result.Add((row, col));

        // if we are pushing up and down, also add the square next to us
        if (direction.row != 0)
        {
            if (square == '[')
            {
                result.AddRange(GetBoxesRecursivelyFromSquareAndDirection(direction, row, col + 1, seen));
            }
            if (square == ']')
            {
                result.AddRange(GetBoxesRecursivelyFromSquareAndDirection(direction, row, col - 1, seen));
            }
        }
        
        // now check for things that get pushed by the boxes
        result.AddRange(GetBoxesRecursivelyFromSquareAndDirection(direction, row + direction.row, col + direction.col, seen));

        return result;
    }

    public long GetGpsResult()
    {
        long sum = 0;
        for (var row = 0; row < grid.Length; row++)
        {
            for (var col = 0; col < grid[0].Length; col++)
            {
                if (grid[row][col] == 'O' || grid[row][col] == '[')
                {
                    sum += (100 * row) + (1 * col);
                }
            }
        }

        return sum;
    }

    public void PrintGrid()
    {
        var gridWithRobot = grid.Select(x => x.ToArray()).ToArray();
        gridWithRobot[robotRow][robotCol] = '@';
        GridConsoleDisplay<char>.Display(gridWithRobot);
    }
}

public static class Parser
{
    public static Grid ParseRobotInstructions(string[] gridLines)
    {
        var grid = new List<char[]>();
        bool inInstuctions = false;
        List<char> instructions = [];
        foreach (var line in gridLines)
        {
            if (string.IsNullOrEmpty(line))
            {
                inInstuctions = true;
                continue;
            }

            if (!inInstuctions)
            {
                grid.Add(line.ToCharArray());
            }
            else
            {
                instructions.AddRange(line.ToCharArray());
            }
        }
        return new Grid([.. grid], [.. instructions]);
    }

    public static Grid ParseRobotInstructionsFat(string[] gridLines)
    {
        var grid = new List<char[]>();
        bool inInstructions = false;
        List<char> instructions = [];
        foreach (var line in gridLines)
        {
            if (string.IsNullOrEmpty(line))
            {
                inInstructions = true;
                continue;
            }

            if (!inInstructions)
            {
                grid.Add(Widen(line.ToCharArray()));
            }
            else
            {
                instructions.AddRange(line.ToCharArray());
            }
        }
        return new Grid([.. grid], [.. instructions]);
    }

    private static char[] Widen(char[] chars)
    {
        var result = new StringBuilder();

        foreach (var c in chars)
        {
            switch (c)
            {
                case '#':
                    result.Append("##");
                    break;
                case 'O':
                    result.Append("[]");
                    break;
                case '.':
                    result.Append("..");
                    break;
                case '@':
                    result.Append("@.");
                    break;

            }
        }

        return result.ToString().ToCharArray();
    }
}