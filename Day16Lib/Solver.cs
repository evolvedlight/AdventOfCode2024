namespace Day16Lib;
using NodeId = (int row, int column);
using Distance = int;
using GridRef = (int row, int column);
using Direction = (int rowDir, int columnDir);
using static Day16Lib.GridSolver;
using System;

public class GridSolver()
{
    public record struct Graph(Dictionary<NodeId, Node> nodes);
    public record struct Node(NodeId location, Edge[] edges);
    public record struct Edge(NodeId neighbor, Direction direction);

    private static Direction DIRECTION_UP = new Direction(-1, 0);
    private static Direction DIRECTION_DOWN = new Direction(1, 0);
    private static Direction DIRECTION_LEFT = new Direction(0, -1);
    private static Direction DIRECTION_RIGHT = new Direction(0, 1);

    public static (int, List<List<NodeId>>, Dictionary<(GridRef, Direction), Distance>) FindShortestRoute(char[][] map)
    {
        // loop through the grid and make an edge from all spaces to all neighbours of length 1
        NodeId startNode = (-1, -1);
        NodeId endNode = (-1, -1);

        var graph = new Graph();
        graph.nodes = new Dictionary<NodeId, Node>();
        for (int row = 1; row < map.Length - 1; row++)
        {
            for (int column = 1; column < map[0].Length - 1; column++)
            {
                if (map[row][column] == '#') continue;

                if (map[row][column] == 'S')
                {
                    startNode = (row, column);
                }

                if (map[row][column] == 'E')
                {
                    endNode = (row, column);
                }

                var node = new Node();
                var nodeEdges = new List<Edge>();
                if (map[row - 1][column] != '#') nodeEdges.Add(new Edge((row - 1, column), DIRECTION_UP));
                if (map[row + 1][column] != '#') nodeEdges.Add(new Edge((row + 1, column), DIRECTION_DOWN));
                if (map[row][column - 1] != '#') nodeEdges.Add(new Edge((row, column - 1), DIRECTION_LEFT));
                if (map[row][column + 1] != '#') nodeEdges.Add(new Edge((row, column + 1), DIRECTION_RIGHT));
                node.edges = nodeEdges.ToArray();
                graph.nodes.Add((row, column), node);
            }
        }

        var (routesToFinish, distances) = RunDikstra(graph, startNode, endNode, map);

        var best = routesToFinish.Min(x => x.distance);
        var allBest = routesToFinish.Where(x => x.distance == best).Select(x => x.visitedPath);

        //foreach (var (visitedPath, distance) in routesToFinish)
        //{
        //    Console.WriteLine($"With distance {distance}:");
        //    var mapClone = map.Select(x => x.ToArray()).ToArray();
        //    foreach (var visited in visitedPath)
        //    {
        //        mapClone[visited.row][visited.column] = 'O';
        //    }
        //    Visualisation.GridConsoleDisplay<char>.Display(mapClone, printAfterCurrentCursor: true);
        //    Console.WriteLine();
        //}

        var c = allBest.SelectMany(x => x).Distinct().Count();
        return (c, allBest.ToList(), distances);
        //return actualDistances.Where(x => x.node.row == endNode.row && x.node.column == endNode.column).Single().Item2;
    }

    public static (List<(List<NodeId> visitedPath, Distance distance)>, Dictionary<(GridRef, Direction), Distance>) RunDikstra(Graph graph, NodeId startNode, NodeId endNode, char[][] map)
    {
        Dictionary<(NodeId, Direction), Distance> distances = [];
        foreach (var node in graph.nodes)
        {
            foreach (var direction in new List<Direction> { DIRECTION_DOWN, DIRECTION_LEFT, DIRECTION_UP, DIRECTION_RIGHT })
            {
                distances[(node.Key, direction)] = int.MaxValue;
            }
        }

        // everything that got to the end
        var results = new List<(List<NodeId>, Distance)>();

        var queue = new PriorityQueue<(NodeId node, Direction direction, List<NodeId> visitedNodes), Distance>();

        distances[(startNode, DIRECTION_RIGHT)] = 0;
        queue.Enqueue((startNode, DIRECTION_RIGHT, [startNode]), 0);

        do
        {
            (NodeId nodeId, Direction direction, List<NodeId> visitedNodes) = queue.Dequeue();
            var nodeDistance = distances[(nodeId, direction)];


            foreach (var edge in graph.nodes[nodeId].edges)
            {

                // ignore the edge we just came from
                if (visitedNodes.Contains(edge.neighbor))
                {
                    continue;
                }

                var distance = distances[(edge.neighbor, direction)];

                // todo check the direction against the edge direction
                int newDistance;
                if (edge.direction == direction)
                {
                    newDistance = nodeDistance + 1;
                }
                else
                {
                    newDistance = nodeDistance + 1001;
                }

                var newPath = visitedNodes.Append(edge.neighbor).ToList();

                if (newDistance <= distance)
                {
                    if (newDistance == 0)
                    {

                    }
                    distances[(edge.neighbor, edge.direction)] = newDistance;
                    queue.Enqueue((edge.neighbor, edge.direction, newPath), newDistance);
                }
               
                if (edge.neighbor.row == endNode.row && edge.neighbor.column == endNode.column)
                {
                    results.Add((newPath, newDistance));
                    //Console.WriteLine($"Found solution to end with distance {newDistance}");
                }
            }
        }
        while (queue.Count > 0);

        return (results, distances);
    }

    
    private readonly record struct NodeState(GridRef position, Direction direction, int Score, List<GridRef> path)
    {
        public NodeState Step() => new(
            (position.row + direction.rowDir, position.column + direction.columnDir),
            direction,
            Score + 1,
            path.Append((position.row + direction.rowDir, position.column + direction.columnDir)).ToList());

        public NodeState TurnLeft() => new(
            position,
            GetLeftOfDirection(direction),
            Score + 1000,
            path);

        public NodeState TurnRight() => new(
            position,
            GetRightOfDirection(direction),
            Score + 1000,
            path);

        private static Direction GetLeftOfDirection(Direction direction)
        {
            return direction switch
            {
                (0, -1) => DIRECTION_DOWN,
                (0, 1) => DIRECTION_UP,
                (1, 0) => DIRECTION_RIGHT,
                (-1, 0) => DIRECTION_LEFT,
            };
        }

        private static Direction GetRightOfDirection(Direction direction)
        {
            return direction switch
            {
                (0, -1) => DIRECTION_UP,
                (0, 1) => DIRECTION_DOWN,
                (1, 0) => DIRECTION_LEFT,
                (-1, 0) => DIRECTION_RIGHT,
            };
        }
    }
    
    public static (int, List<List<GridRef>>, Dictionary<(GridRef, Direction), Distance>) FindBestPathCountSquares(char[][] map)
    {
        GridRef start = (-1, -1);
        for (int row = 1; row < map.Length - 1; row++)
        {
            for (int column = 1; column < map[0].Length - 1; column++)
            {
                if (map[row][column] == 'S')
                {
                    start = (row, column);
                }
            }
        }

        var bestPaths = new List<List<GridRef>>();
        var bestScore = int.MaxValue;
        var startState = new NodeState(start, DIRECTION_RIGHT, 0, [start]);
        var queue = new Queue<NodeState>();
        var minScores = new Dictionary<(GridRef, Direction), int>();
        queue.Enqueue(startState);

        while (queue.Count > 0)
        {
            var state = queue.Dequeue();
            if (state.Score > bestScore)
            {
                continue;
            }

            if (map[state.position.row][state.position.column] == 'E')
            {
                if (state.Score == bestScore)
                {
                    bestPaths.Add(state.path);
                }
                else if (state.Score < bestScore)
                {
                    bestPaths = [state.path];
                    bestScore = state.Score;
                }
                continue;
            }

            (int row, int col) inFront = (state.position.row + state.direction.rowDir, state.position.column + state.direction.columnDir);
            if (map[inFront.row][inFront.col] != '#')
            {
                EnqueueIfBetter(state.Step());
            }

            (int row, int col) toLeft = (state.position.row - state.direction.columnDir, state.position.column + state.direction.rowDir);
            if (map[toLeft.row][toLeft.col] != '#')
            {
                EnqueueIfBetter(state.TurnLeft());
            }

            (int row, int col) toRight = (state.position.row + state.direction.columnDir, state.position.column - state.direction.rowDir);
            if (map[toRight.row][toRight.col] != '#')
            {
                EnqueueIfBetter(state.TurnRight());
            }
            void EnqueueIfBetter(NodeState candidate)
            {
                if (minScores.GetValueOrDefault((candidate.position, candidate.direction), int.MaxValue) >= candidate.Score)
                {
                    if (candidate.Score == 2000)
                    {

                    }
                    minScores[(candidate.position, candidate.direction)] = candidate.Score;
                    queue.Enqueue(candidate);
                }
            }
        }

        var c = bestPaths.SelectMany(x => x).Distinct().Count();
        return (c, bestPaths, minScores);
        
    }
}