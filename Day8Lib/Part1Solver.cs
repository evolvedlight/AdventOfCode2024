using System.Collections.Generic;

namespace Day8Lib
{
    public class Part1Solver
    {
        public static int GetNumberOfAntiNodes(char[][] grid)
        {
            var antennas = grid.SelectMany((yRow, y) => yRow.Select((gridVal, x) => (y, x, gridVal)).Where(record => record.gridVal != '.')).GroupBy(r => r.gridVal);
            var maxX = grid[0].Length;
            var maxY = grid.Length;
            HashSet<(int y, int x)> foundNodes = [];
            foreach (var antennaType in antennas)
            {
                //Console.WriteLine(antennaType.Key);

                // for each antenna we loop through each other antennas,
                // calculate the difference in x and y between the two antennas and then add that to each one
                // , see if it's in bounds
                var antennasList = antennaType.ToList();
                var combinations = antennasList
                    .SelectMany(item1 => antennasList, (item1, item2) => new { item1, item2 })
                    .Where(t => t.item1 != t.item2)
                    .Select(t => Tuple.Create(t.item1, t.item2));

                foreach (var combination in combinations)
                {
                    var diffX = combination.Item1.x - combination.Item2.x;
                    var diffY = combination.Item1.y - combination.Item2.y;

                    var antiNodeX = combination.Item2.x - diffX;
                    var antiNodeY = combination.Item2.y - diffY;

                    if (antiNodeX >= 0 && antiNodeX < maxX && antiNodeY >= 0 && antiNodeY < maxY)
                    {
                        foundNodes.Add((antiNodeY, antiNodeX));
                    }
                }
            }

            foreach ((int y, int x) in foundNodes)
            {
                grid[y][x] = '#';
            }
            PrintGrid(grid);

            return foundNodes.Count;
        }

        public static int GetNumberOfAntiNodes2(char[][] grid)
        {
            var antennas = grid.SelectMany((yRow, y) => yRow.Select((gridVal, x) => (y, x, gridVal)).Where(record => record.gridVal != '.')).GroupBy(r => r.gridVal);
            var maxX = grid[0].Length;
            var maxY = grid.Length;
            HashSet<(int y, int x)> foundNodes = [];
            foreach (var antennaType in antennas)
            {
                var antennasList = antennaType.ToList();
                var combinations = antennasList
                    .SelectMany(item1 => antennasList, (item1, item2) => new { item1, item2 })
                    .Where(t => t.item1 != t.item2)
                    .Select(t => Tuple.Create(t.item1, t.item2));

                foreach (var combination in combinations)
                {
                    var diffX = combination.Item1.x - combination.Item2.x;
                    var diffY = combination.Item1.y - combination.Item2.y;

                    var currentLocation = (combination.Item2.y, combination.Item2.x);

                    while (currentLocation.x >= 0 && currentLocation.x < maxX && currentLocation.y >= 0 && currentLocation.y < maxY)
                    {
                        foundNodes.Add((currentLocation.y, currentLocation.x));

                        currentLocation.y = currentLocation.y - diffY;
                        currentLocation.x = currentLocation.x - diffX;
                    }
                }
            }

            foreach ((int y, int x) in foundNodes)
            {
                grid[y][x] = '#';
            }
            PrintGrid(grid);

            return foundNodes.Count;
        }

        public static int GetNumberOfAntiNodesWithVisualisation(char[][] grid)
        {
            PrintGrid(grid);

            var antennas = grid.SelectMany((yRow, y) => yRow.Select((gridVal, x) => (y, x, gridVal)).Where(record => record.gridVal != '.')).GroupBy(r => r.gridVal);
            var maxX = grid[0].Length;
            var maxY = grid.Length;
            HashSet<(int y, int x)> foundNodes = [];

            foreach (var antennaType in antennas)
            {
                var antennasList = antennaType.ToList();
                var combinations = antennasList
                    .SelectMany(item1 => antennasList, (item1, item2) => new { item1, item2 })
                    .Where(t => t.item1 != t.item2)
                    .Select(t => Tuple.Create(t.item1, t.item2));

                foreach (var combination in combinations)
                {
                    var diffX = combination.Item1.x - combination.Item2.x;
                    var diffY = combination.Item1.y - combination.Item2.y;

                    var currentLocation = (combination.Item2.y, combination.Item2.x);

                    while (currentLocation.x >= 0 && currentLocation.x < maxX && currentLocation.y >= 0 && currentLocation.y < maxY)
                    {
                        foundNodes.Add((currentLocation.y, currentLocation.x));

                        Console.ForegroundColor = ConsoleColor.DarkMagenta;
                        Console.SetCursorPosition(currentLocation.y, currentLocation.x);
                        Console.Write('#');
                        Thread.Sleep(100);

                        currentLocation.y = currentLocation.y - diffY;
                        currentLocation.x = currentLocation.x - diffX;
                    }
                }
            }

            return foundNodes.Count;
        }

        public static void PrintGrid(char[][] grid)
        {
            foreach (var row in grid)
            {
                foreach (var col in row)
                {
                    Console.Write(col);
                }
                Console.WriteLine();
            }
        }
    }
}
