
namespace Day11Lib
{
    public class SolverBuckets
    {
        private Dictionary<long, long> _stones;

        public SolverBuckets(List<long> stones)
        {
            _stones = stones.CountBy(x => x).ToDictionary(x => x.Key, x => (long)x.Value);
        }

        public long Blink75()
        {
            for (int i = 0; i < 75; i++)
            {
                BlinkStones();
            }

            return _stones.Values.Sum();
        }

        public void BlinkStones()
        {
            var newDict = new DefaultDictionary<long, long>();
            foreach (var (stone, numberOfThem) in _stones)
            {
                if (stone == 0)
                {
                    newDict[1] += numberOfThem;
                    continue;
                }

                var currentString = stone.ToString();
                if (currentString.Length % 2 == 0)
                {
                    var leftNumber = long.Parse(currentString[..(currentString.Length / 2)]);
                    var rightNumber = long.Parse(currentString[(currentString.Length / 2)..]);

                    newDict[leftNumber] += numberOfThem;
                    newDict[rightNumber] += numberOfThem;

                    continue;
                }

                newDict[stone * 2024] = numberOfThem;
            }

            _stones = newDict;
        }
    }

    public class DefaultDictionary<TKey, TValue> : Dictionary<TKey, TValue> where TValue : new()
    {
        public new TValue this[TKey key]
        {
            get
            {
                TValue val;
                if (!TryGetValue(key, out val))
                {
                    val = new TValue();
                    Add(key, val);
                }
                return val;
            }
            set { base[key] = value; }
        }
    }
}