

namespace Day12Lib;

public class Garden
{
    private char[][] _garden;
    private readonly int _maxRows;
    private readonly int _maxColumns;

    public Garden(char[][] garden)
    {
        _garden = garden;
        _maxRows = garden.Length;
        _maxColumns = garden[0].Length;
    }

    public static Garden FromLines(string[] lines)
    {
        return new Garden(lines.Select(x => x.ToCharArray()).ToArray());
    }

    public List<Region> GetRegions()
    {
        List<Region> results = [];
        HashSet<(int row, int col)> UsedPlotsTotal = [];

        for (int row = 0; row < _garden.Length; row++)
        {
            for (int column = 0; column < _garden[row].Length; column++)
            {
                if (UsedPlotsTotal.Contains((row, column)))
                {
                    continue;
                }

                var plantType = _garden[row][column];
                var region = new Region() { PlantType = plantType };
                results.Add(region);
                
                HashSet<(int row, int col)> UsedPlots = [];
                region.Plots = RecursivelyFindAllOfType(plantType, row, column, UsedPlots);

                foreach (var plot in region.Plots)
                {
                    UsedPlotsTotal.Add((plot.row, plot.col));
                }
            }
        }

        return results;
    }

    private List<(int row, int col, List<NeighbourLocation> neighbours, int corners)> RecursivelyFindAllOfType(char plantType, int row, int column, HashSet<(int row, int col)> usedPlots)
    { 
        //Console.WriteLine($"Checking for plant type {plantType} in {row}, {column}");
        if (usedPlots.Contains((row, column)))
        {
            return [];
        }

        usedPlots.Add((row, column));
        if (row < 0 || column < 0 || row >= _maxRows || column >= _maxColumns)
        {
            return [];
        }
        
        var result = new List<(int row, int col, List<NeighbourLocation> neighbours, int corners)>();
        // Add the current one if it's correct
        if (_garden[row][column] == plantType)
        {
            var neighbours = GetNeighbours(row, column);
            var corners = GetCountOfCorners(row, column, neighbours);
            result.Add((row, column, neighbours, corners));
        }
        else
        {
            if (usedPlots.Contains((row, column)))
            {
                return [];
            }
        }

        result.AddRange(RecursivelyFindAllOfType(plantType, row - 1, column, usedPlots));
        result.AddRange(RecursivelyFindAllOfType(plantType, row + 1, column, usedPlots));
        result.AddRange(RecursivelyFindAllOfType(plantType, row, column - 1, usedPlots));
        result.AddRange(RecursivelyFindAllOfType(plantType, row, column + 1, usedPlots));

        return result;
    }

    private List<NeighbourLocation> GetNeighbours(int row, int column)
    {
        var plantType = _garden[row][column];
        var result = new List<NeighbourLocation>();
        if (IsSame(plantType, row - 1, column)) result.Add(NeighbourLocation.Up);
        if (IsSame(plantType, row + 1, column)) result.Add(NeighbourLocation.Down);
        if (IsSame(plantType, row, column + 1)) result.Add(NeighbourLocation.Right);
        if (IsSame(plantType, row, column - 1)) result.Add(NeighbourLocation.Left);
        return result;
    }

    private int GetCountOfCorners(int row, int column, List<NeighbourLocation> neighbours)
    {
        // if we have no neighbours, corners everywhere
        if (neighbours.Count == 0)
        {
            return 4;
        }

        // if we have one neighbour, we must have 2 corners
        if (neighbours.Count == 1)
        {
            return 2;
        }

        var plantType = _garden[row][column];
        // if we have two neighbours
        if (neighbours.Count == 2)
        {
            // if they are in a straight line, no corners
            if (neighbours.Contains(NeighbourLocation.Left) && neighbours.Contains(NeighbourLocation.Right) ||
                neighbours.Contains(NeighbourLocation.Up) && neighbours.Contains(NeighbourLocation.Down))
            {
                return 0;
            }

            // otherwise we need to check the corners are the same as ours. if it's the same, then we only have one corner, otherwise 2
            if (neighbours.Contains(NeighbourLocation.Up))
            {
                if (neighbours.Contains(NeighbourLocation.Left))
                {
                    return (_garden[row - 1][column - 1] == plantType) ? 1 : 2;
                }
                if (neighbours.Contains(NeighbourLocation.Right))
                {
                    return (_garden[row - 1][column + 1] == plantType) ? 1 : 2;
                }
            }

            if (neighbours.Contains(NeighbourLocation.Down))
            {
                if (neighbours.Contains(NeighbourLocation.Left))
                {
                    return (_garden[row + 1][column - 1] == plantType) ? 1 : 2;
                }
                if (neighbours.Contains(NeighbourLocation.Right))
                {
                    return (_garden[row + 1][column + 1] == plantType) ? 1 : 2;
                }
            }
        }

        if (neighbours.Count == 3)
        {
            // this means we have a T shape and need to check the two corners
            if (!neighbours.Contains(NeighbourLocation.Up))
            {
                // check for corners in bottom left and bottom right
                return ((_garden[row + 1][column - 1] == plantType) ? 0 : 1) + ((_garden[row + 1][column + 1] == plantType) ? 0 : 1);
            }

            if (!neighbours.Contains(NeighbourLocation.Down))
            {
                // check for corners in top left and  top right
                return ((_garden[row - 1][column - 1] == plantType) ? 0 : 1) + ((_garden[row - 1][column + 1] == plantType) ? 0 : 1);
            }

            if (!neighbours.Contains(NeighbourLocation.Left))
            {
                // check for corners in bottom right and top right
                return ((_garden[row + 1][column + 1] == plantType) ? 0 : 1) + ((_garden[row - 1][column + 1] == plantType) ? 0 : 1);
            }

            if (!neighbours.Contains(NeighbourLocation.Right))
            {
                // check for corners in bottom left and top left
                return ((_garden[row + 1][column - 1] == plantType) ? 0 : 1) + ((_garden[row - 1][column - 1] == plantType) ? 0 : 1);
            }
        }

        // if we have 4 neighbours, check all the corners. any with a differnt type are a corner
        if (neighbours.Count == 4)
        {
            return (_garden[row + 1][column + 1] == plantType ? 0 : 1) +
                   (_garden[row + 1][column - 1] == plantType ? 0 : 1) +
                   (_garden[row - 1][column + 1] == plantType ? 0 : 1) +
                   (_garden[row - 1][column - 1] == plantType ? 0 : 1);
    }
        throw new Exception("shit");
    }

    private bool IsSame(char plantType, int row, int column)
    {
        if (row < 0 || column < 0 || row >= _maxRows || column >= _maxColumns)
        {
            return false;
        }
        return _garden[row][column] == plantType;
    }
}

public class Region {
    public char PlantType { get; set; }
    public List<(int row, int col, List<NeighbourLocation> neighbours, int corners)> Plots = [];

    
    public int Area { 
        get
        {
            return Plots.Count;
        } 
    }

    public int CalculatePerimeter()
    {
        var fencesNeeded = 0;
        // for each plot, see how many other plot it is touching.. and add that?
        foreach (var item in Plots)
        {
            fencesNeeded += 4 - item.neighbours.Count;
        }

        return fencesNeeded;
    }

    public int Sides
    {
        get
        {
            return Plots.Sum(p => p.corners);
        }
    }
}

public enum NeighbourLocation
{
    Up,
    Down,
    Left,
    Right
}