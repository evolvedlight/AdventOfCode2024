using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day6Lib;

public class Solver
{
    public static List<(int, int)> Part1WalkGuard(MapBoard input)
    {
        while (input.GuardIsInside)
        {
            input.StepGuard();
        }

        return input.GetWalkedPositions();
    }

    public static int Part2GetGuardStuck(MapBoard input)
    {
        var originalGuardLocation = input._guardPosition;
        // we'll brute force the problem and ask to solve for each position: are we stuck or are we out?
        var count = 0;
        for (int y = 0; y < input._Map.Count; y++)
        {
            for (int x = 0; x < input._Map[y].Count; x++)
            {
                var isObstructed = isObstructedWithObstrubtionIn(input, y, x, originalGuardLocation);
                if (isObstructed)
                {
                    count++;
                }
            }
        }

        return count;
    }

    public static int Part2GetGuardStuckOptimised(MapBoard input)
    {
        var originalGuardLocation = input._guardPosition;

        // only check locations on original route;
        var positions = Part1WalkGuard(input);

        var count = 0;
        foreach (var position in positions.Distinct()) {
            var isObstructed = isObstructedWithObstrubtionIn(input, position.Item1, position.Item2, originalGuardLocation);
            if (isObstructed)
            {
                count++;
            }
        }

        return count;
    }

    public static bool isObstructedWithObstrubtionIn(MapBoard input, int y, int x, (int guardY, int guardX, char guardDirection) originalGuardLocation)
    {
        var newBoard = MapBoard.CloneFromWithObstruction(input, y, x, originalGuardLocation);

        while (newBoard.GuardIsInside && !newBoard.GuardIsInLoop())
        {
            newBoard.StepGuard();
        }

        return newBoard.GuardIsInside;
    }
}
