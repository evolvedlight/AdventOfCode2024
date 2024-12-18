using XYCoords = (int x, int y);

var inputLines = await File.ReadAllLinesAsync("input.txt");

var data = inputLines.Select((line => new XYCoords(int.Parse(line.Split(",")[0]), int.Parse(line.Split(",")[1])))).ToList();
var size = 70;
int Find(int i)
{
    var seen = new HashSet<XYCoords>(data.Take(i));
    var todo = new Queue<(int, XYCoords)>();
    todo.Enqueue((0, new XYCoords(0, 0)));

    while (todo.Count > 0)
    {
        (int dist, XYCoords coords) = todo.Dequeue();
        if (coords.x == size && coords.y == size)
        {
            return dist;
        }

        var directions = new List<XYCoords>() { new(-1, 0), new(1, 0), new(0, -1), new(0, 1) };
        foreach (var direction in directions)
        {
            var newCoords = new XYCoords(coords.x + direction.x, coords.y + direction.y);
            if (!seen.Contains(newCoords) && newCoords.x >= 0 && newCoords.x <= size && newCoords.y >= 0 && newCoords.y <= size)
            {
                todo.Enqueue((dist + 1, newCoords));
                seen.Add(newCoords);
            }
        }
    }

    return -1;
}

//Console.WriteLine(Find(1024));

// binary chop this fun
var start = 1;
var end = data.Count();
var middle = (end - start) / 2 + start;
while (start < end - 1)
{
    middle = ((end - start) / 2) + start;

    if (Find(middle) == -1)
    {
        // not possble so we go earlier
        end = middle;
    }
    else
    {
        start = middle;
    }
}
Console.WriteLine(middle - 1);
Console.WriteLine(data[middle - 1]);

// brute force for fun
var count = 0;
while (Find(count) != -1)
{
    count++;
}

Console.WriteLine(count - 1);
Console.WriteLine(data[count - 1]);