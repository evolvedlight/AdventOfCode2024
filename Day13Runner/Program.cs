using Day13Lib;
using System.Numerics;

var lines = await File.ReadAllLinesAsync("input.txt");

BigInteger sum = 0;
for (int i = 0; i < lines.Length; i += 4)
{
    Console.WriteLine($"Reading at {i}");
    var machine = ArcadeMachine.FromLines(lines[i..(i + 3)]);

    var (isPossible, x, y) = MathsMethods.CramersRule(machine.ButtonAX, machine.ButtonAY, machine.ButtonBX, machine.ButtonBY, machine.PrizeX, machine.PrizeY);

    if (!isPossible)
    {
        Console.WriteLine("nope");
        continue;
    }

    if (x.Value == Math.Round(x.Value) && y.Value == Math.Round(y.Value))
    {
        Console.WriteLine($"Found at {x}, {y}");
        sum += new BigInteger(x.Value) *  3 + new BigInteger(y.Value);
    }
    else
    {
        Console.WriteLine("Not an even solution");
    }
}

Console.WriteLine($"Total: {sum}");

BigInteger sum2 = 0;
for (int i = 0; i < lines.Length; i += 4)
{
    Console.WriteLine($"Reading at {i}");
    var machine = ArcadeMachine.FromLines(lines[i..(i + 3)]);

    var (isPossible, x, y) = MathsMethods.CramersRule(machine.ButtonAX, machine.ButtonAY, machine.ButtonBX, machine.ButtonBY, machine.PrizeX + 10000000000000, machine.PrizeY + 10000000000000);

    if (!isPossible)
    {
        Console.WriteLine("nope");
        continue;
    }

    if (x.Value == Math.Round(x.Value) && y.Value == Math.Round(y.Value))
    {
        Console.WriteLine($"Found at {x}, {y}");
        sum2 += new BigInteger(x.Value) * 3 + new BigInteger(y.Value);
    }
    else
    {
        Console.WriteLine("Not an even solution");
    }
}

Console.WriteLine($"Total: {sum2}");