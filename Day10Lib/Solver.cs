

namespace Day10Lib
{
    public class Solver
    {
        private readonly int[][] _map;
        
        public Solver(int[][] map)
        {
            _map = map;
        }

        public int SolvePart1()
        {
            // Look for any trailheads and then go from there
            var sum = 0;
            for (int row = 0; row < _map.Length; row++)
            {
                for (int column = 0; column < _map[0].Length; column++)
                {
                    if (_map[row][column] == 0)
                    {
                        HashSet<(int row, int column)> foundPeaks = [];
                        CheckPeaksReachableFromHere(row, column, -1, foundPeaks);

                        sum += foundPeaks.Count;
                    }
                }
            }
            return sum;
        }

        private void CheckPeaksReachableFromHere(int row, int column, int oldHeight, HashSet<(int row, int column)> foundPeaks)
        {
            // if we went off the map:
            if (row < 0 || row >= _map.Length || column < 0 || column >= _map[0].Length)
            {
                return;
            }

            var currentHeight = _map[row][column];
            if (currentHeight != oldHeight + 1)
            {
                return;
            }

            if (currentHeight == 9)
            {
                foundPeaks.Add((row, column));
            }

            

            CheckPeaksReachableFromHere(row - 1, column, currentHeight, foundPeaks);
            CheckPeaksReachableFromHere(row + 1, column, currentHeight, foundPeaks);
            CheckPeaksReachableFromHere(row, column - 1, currentHeight, foundPeaks);
            CheckPeaksReachableFromHere(row, column + 1, currentHeight, foundPeaks);
        }

        public int SolvePart2()
        {
            // Look for any trailheads and then go from there
            var sum = 0;
            for (int row = 0; row < _map.Length; row++)
            {
                for (int column = 0; column < _map[0].Length; column++)
                {
                    if (_map[row][column] == 0)
                    {
                        List<(int row, int column)> foundHikingTrails = [];
                        CheckPeaksReachableFromHereList(row, column, -1, foundHikingTrails);

                        sum += foundHikingTrails.Count;
                    }
                }
            }
            return sum;
        }

        private void CheckPeaksReachableFromHereList(int row, int column, int oldHeight, List<(int row, int column)> foundHikingTrails)
        {
            // if we went off the map:
            if (row < 0 || row >= _map.Length || column < 0 || column >= _map[0].Length)
            {
                return;
            }

            var currentHeight = _map[row][column];
            if (currentHeight != oldHeight + 1)
            {
                return;
            }

            if (currentHeight == 9)
            {
                foundHikingTrails.Add((row, column));
            }



            CheckPeaksReachableFromHereList(row - 1, column, currentHeight, foundHikingTrails);
            CheckPeaksReachableFromHereList(row + 1, column, currentHeight, foundHikingTrails);
            CheckPeaksReachableFromHereList(row, column - 1, currentHeight, foundHikingTrails);
            CheckPeaksReachableFromHereList(row, column + 1, currentHeight, foundHikingTrails);
        }
    }
}
