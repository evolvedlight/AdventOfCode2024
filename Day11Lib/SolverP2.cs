
namespace Day11Lib
{
    public class StoneLinesP2
    {
        private List<long> _stones;
        private Dictionary<(long, int), long> _cache = [];

        public StoneLinesP2(List<long> stones)
        {
            _stones = stones;
        }

        public long Blink25()
        {
            long total = 0;
            for (int i = 0; i < _stones.Count; i++)
            {
                total += CheckStoneSize(_stones[i], 25);
            }

            return total;
        }

        public long Blink75()
        {
            long total = 0;
            for (int i = 0; i < _stones.Count; i++)
            {
                total += CheckStoneSize(_stones[i]);
            }

            return total;
        }

        public long CheckStoneSize(long stone, int depth = 75)
        {
            if (_cache.TryGetValue((stone, depth), out var size))
            {
                return size;
            }
            if (depth == 0)
            {
                _cache.Add((stone, depth), 1);
                return 1;
            }
            if (stone == 0)
            {
                var intSize = CheckStoneSize(1, depth - 1);
                _cache.Add((stone, depth), intSize);
                return intSize;
            }
            var currentString = stone.ToString();
            if (currentString.Length % 2 == 0)
            {
                var leftNumber = long.Parse(currentString[..(currentString.Length / 2)]);
                var rightNumber = long.Parse(currentString[(currentString.Length / 2)..]);
                var intSize2 = CheckStoneSize(leftNumber, depth - 1) + CheckStoneSize(rightNumber, depth - 1);
                _cache.Add((stone, depth), intSize2);
                return intSize2;
            }
            var intSize3 = CheckStoneSize(stone * 2024, depth - 1);
            _cache.Add((stone, depth), intSize3);
            return intSize3;
        }
    }
}