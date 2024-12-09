
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.Design.Serialization;
using System.Text;

namespace Day9Lib
{
    public class Solver
    {
        public static long SolvePart1(string input)
        {
            List<int?> map = [];
            var fileIndex = 0;
            var numberOfFileBlocksUsed = 0;
            foreach (var (digit, index) in input.Select((x, y) => (int.Parse(x.ToString()), y)))
            {
                if (index % 2 == 0)
                {
                    map.AddRange(Enumerable.Repeat(fileIndex, digit).Select(x => (int?)x));
                    numberOfFileBlocksUsed += digit;
                    fileIndex++;
                }
                else
                {
                    map.AddRange(Enumerable.Repeat((int?)null, digit));
                }
            }

            var mapLength = map.Count() - 1;

            var leftIndex = 0;
            var rightIndex = mapLength;

            while (leftIndex < rightIndex)
            {
                if (map[leftIndex].HasValue)
                {
                    // don't want to move things on the left
                    leftIndex++;
                    continue;
                }
                map[leftIndex] = map[rightIndex];
                map[rightIndex] = null;
                rightIndex--;
            }

            return map.Select((number, index) => number.GetValueOrDefault(0) * (long)index).Sum();
        }

        public static long SolvePart2(string input)
        {
            List<int?> map = [];
            var fileIndex = 0;
            List<SpaceItem> spaces = [];
            var blockIndex = 0;
            foreach (var (digit, index) in input.Select((x, y) => (int.Parse(x.ToString()), y)))
            {
                if (index % 2 == 0)
                {
                    map.AddRange(Enumerable.Repeat(fileIndex, digit).Select(x => (int?)x));
                    fileIndex++;
                }
                else
                {
                    if (digit != 0)
                    {
                        spaces.Add(new SpaceItem { Index = blockIndex, Size = digit });
                        map.AddRange(Enumerable.Repeat((int?)null, digit));
                    }
                }
                blockIndex += digit;
            }

            var mapLength = map.Count - 1;

            //Console.WriteLine(string.Join("", map.Select(x => x.HasValue ? x.Value.ToString() : ".")));

            var rightIndex = mapLength;

            while (rightIndex > 0)
            {
                // check the length of the rightmost thing
                rightIndex = FindNextRightThing(map, rightIndex);
                var rightThing = GetRightThing(map, rightIndex).ToList();
                var sizeOfRightThing = rightThing.Count;
                var startPositionOfRightThing = rightIndex - sizeOfRightThing;

                //Console.WriteLine($"Trying to move {string.Join("", rightThing)}");

                var newLeftPosition = FindLeftMostBiggestSpace(spaces, sizeOfRightThing);
                //Console.WriteLine($"Found space: {newLeftPosition}");
                if (newLeftPosition > startPositionOfRightThing)
                {
                    rightIndex -= sizeOfRightThing;
                    continue;
                }

                if (newLeftPosition.HasValue)
                {
                    // copy from the right to the left
                    for (var i = 0; i < sizeOfRightThing; i++)
                    {
                        map[newLeftPosition.Value + i] = rightThing[0];
                        map[rightIndex - i] = null;
                        
                    }
                }
                rightIndex -= sizeOfRightThing;
                //Console.WriteLine(string.Join("", map.Select(x => x.HasValue ? x.Value.ToString() : ".")));

            }
            //Console.WriteLine(string.Join("", map.Select(x => x.HasValue ? x.Value.ToString() : ".")));
            return map.Select((number, index) => number.GetValueOrDefault(0) * (long)index).Sum();
        }

        private static int FindNextRightThing(List<int?> map, int rightIndex)
        {
            while (!map[rightIndex].HasValue)
            {
                rightIndex--;
            }

            return rightIndex;
        }

        public static int? FindLeftMostBiggestSpace(List<SpaceItem> spaces, int sizeOfRightThing)
        {
            var space = spaces.Where(x => x.Size >= sizeOfRightThing).FirstOrDefault();

            if (space == null)
            {
                return null;
            }
            else
            {
                var index = space.Index;
                space.Size = space.Size - sizeOfRightThing;
                space.Index = index + sizeOfRightThing;
                return index;
            }
        }

        private static IEnumerable<int> GetRightThing(List<int?> map, int rightIndex)
        {
            var startNumber = map[rightIndex];
            if (!startNumber.HasValue)
            {
                yield break;
            }
            
            var number = map[rightIndex];
            
            while (number.HasValue && rightIndex >= 0 && map[rightIndex] == startNumber)
            {
                yield return startNumber.Value;
                rightIndex--;
            }
        }
    }

    public class SpaceItem
    {
        public int Index { get; set; }
        public int Size { get; set; }
    }
}
