namespace Visualisation
{
    public static class GridConsoleDisplay<T> where T : struct, IConvertible
    {
        private static T[][]? CachedGrid;

        public static void Display(T[][] grid)
        {
            if (grid.Length == 0)
            {
                Console.Clear();
                return;
            }
            if (CachedGrid == null)
            {
                for (int row = 0; row < grid.Length; row++)
                {
                    for (int column = 0; column < grid[0].Length; column++)
                    {
                        Console.SetCursorPosition(column, row);
                        Console.Write(grid[row][column]);
                    }
                }
            }
            else
            {
                // if grid is different size, just start again
                if (grid.Length != CachedGrid.Length || grid[0].Length != CachedGrid[0].Length)
                {
                    Console.Clear();
                }
                for (int row = 0; row < CachedGrid.Length; row++)
                {
                    for (int column = 0; column < CachedGrid[0].Length; column++)
                    {
                        var cachedVal = CachedGrid[row][column];
                        var otherVal = grid[row][column];
                        if (!EqualityComparer<T>.Default.Equals(cachedVal, otherVal))
                        {
                            Console.SetCursorPosition(column, row);
                            Console.Write(grid[row][column]);
                        }
                    }
                }
            }

            Console.SetCursorPosition(0, grid.Length + 1);

            CachedGrid = grid;
        }
    }
}
