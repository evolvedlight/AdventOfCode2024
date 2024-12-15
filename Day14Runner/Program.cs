using Day14Lib;

var fileLines = await File.ReadAllLinesAsync("mari.txt");

var robots = Parser.Parse(fileLines);

//Solver.Move100Seconds(robots, 101, 103, 100);
//Console.WriteLine($"Answer is: {Solver.CalculateSum(robots, 101, 103)}");

// loop until we have something like a tree..

var i = 0;
while (true)
{
    i++;
    Solver.Move100Seconds(robots, 101, 103, 1);
    var isPossibleTree = Solver.DoWeHavePossibleTree(robots, 101, 103);

    if (isPossibleTree)
    {
        Console.WriteLine($"At iteration {i}, Maybe ok");
        Solver.PrintRobots(robots, 101, 103);
        return;
    }
    else
    {
        Console.WriteLine($"At iteration {i}, Continue!");
    }
    
}
   