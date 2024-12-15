using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Day14Lib;

public class Solver
{
    public static void PrintRobots(List<Robot> robots, int maxX, int maxY)
    {
        var robotsInPosition = robots.GroupBy(x => x.Position).ToDictionary(x => x.Key, x => x.Count());
        for (int y = 0; y < maxY; y++)
        {
            for (int x = 0; x < maxX; x++)
            {
                if (robotsInPosition.TryGetValue((x, y), out var count)) 
                {
                    Console.Write(count);
                }
                else
                {
                    Console.Write(".");
                }
            }
            Console.WriteLine();
        }
    }

    public static void Move100Seconds(List<Robot> robots, int maxX, int maxY, int seconds = 1)
    {
        foreach (var robot in robots)
        {
            robot.Position = CalculateNewPosition(maxX, maxY, seconds, robot);
        }
    }

    private static (int, int) CalculateNewPosition(int maxX, int maxY, int seconds, Robot robot)
    {
        var newX = (robot.Position.x + (robot.Velocity.x * seconds)) % maxX;
        if (newX < 0)
        {
            newX += maxX;
        }
        var newY = (robot.Position.y + (robot.Velocity.y * seconds)) % maxY;
        if (newY < 0)
        {
            newY += maxY;
        }
        return (newX, newY);
    }

    public static long CalculateSum(List<Robot> robots, int maxX, int maxY)
    {
        var middleX = maxX / 2;
        var middleY = maxY / 2;
        var robotsInPosition = robots.GroupBy(x => x.Position).ToDictionary(x => x.Key, x => x.Count());
        var quadrants = new Dictionary<(bool, bool), long>();
        for (int y = 0; y < maxY; y++)
        {
            for (int x = 0; x < maxX; x++)
            {
                if (x == middleX || y == middleY)
                {
                    continue;
                }
                if (robotsInPosition.TryGetValue((x, y), out var count))
                {
                    var key = (x < middleX, y < middleY);
                    if (quadrants.TryGetValue(key, out var val))
                    {
                        quadrants[key] = val + count;
                    }
                    else
                    {
                        quadrants[key] = count;
                    }
                }                
            }
        }

        return quadrants.Values.Aggregate((s, a) => s*a);
    }

    public static bool DoWeHavePossibleTree(List<Robot> robots, int maxX, int maxY)
    {
        var robotsInPosition = robots.GroupBy(x => x.Position).ToDictionary(x => x.Key, x => x.Count());
        for (int y = 0; y < maxY; y++)
        {
            var continuousCount = 0;
            for (int x = 0; x < maxX; x++)
            {
                if (robotsInPosition.TryGetValue((x, y), out var count))
                {
                    continuousCount++;
                }
                else
                {
                    if (continuousCount > 10)
                    {
                        return true;
                    }
                    continuousCount = 0;
                }
            }
        }
        return false;
    }
}
public class Region
{
    public char PlantType { get; set; }
    public List<(int row, int col)> Boxes = [];
}

public static partial class Parser
{
    public static List<Robot> Parse(string[] input)
    {
        var robots = new List<Robot>();

        var regex = RobotParsingRegex();

        foreach (var item in input)
        {
            var match = regex.Match(item);

            robots.Add(new Robot { Position = (int.Parse(match.Groups["sx"].Value), int.Parse(match.Groups["sy"].Value)), Velocity = (int.Parse(match.Groups["vx"].Value), int.Parse(match.Groups["vy"].Value)) });
        }

        return robots;
    }

    [GeneratedRegex("p=(?'sx'\\d+),(?'sy'\\d+) v=(?'vx'[\\-\\d]+),(?'vy'[\\-\\d]+)")]
    private static partial Regex RobotParsingRegex();
}

public class Robot
{
    public (int x, int y) Position { get; set; }
    public (int x, int y) Velocity { get; set; }
}