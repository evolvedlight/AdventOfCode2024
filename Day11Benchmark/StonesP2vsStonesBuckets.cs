using BenchmarkDotNet.Attributes;
using System.Security.Cryptography;

public class StonesP2vsStonesBuckets
{
    public readonly List<long> stones;

    public StonesP2vsStonesBuckets()
    {
        stones = File.ReadAllText("input.txt").Split(" ").Select(x => long.Parse(x)).ToList();
    }

    [Benchmark]
    public long P2()
    {
        var solverP2 = new Day11Lib.StoneLinesP2(stones);
        return solverP2.Blink75();
    }

    [Benchmark]
    public long P2Buckets()
    {
        var solverP2Buckets = new Day11Lib.SolverBuckets(stones);
        return solverP2Buckets.Blink75();
    }
}