
namespace Day11Lib
{
    public class StoneLines
    {
        private List<long> _stones;

        public StoneLines(List<long> stones)
        {
            _stones = stones;
        }

        public void Blink25()
        {
            for (int i = 0; i < 25; i++)
            {
                Blink();
                //Print();
            }
        }

        public void Blink()
        {
            for (int i = 0; i < _stones.Count; i++)
            {
                var current = _stones[i];
                if (current == 0)
                {
                    _stones[i] = 1;
                    continue;
                }
                var currentString = current.ToString();
                if (currentString.Length % 2 == 0)
                {
                    var leftNumber = long.Parse(currentString[..(currentString.Length / 2)]);
                    var rightNumber = long.Parse(currentString[(currentString.Length / 2)..]);
                    _stones[i] = leftNumber;
                    _stones.Insert(i + 1, rightNumber);
                    i++;
                    continue;
                }
                _stones[i] = _stones[i] * 2024;
            }
        }

        public void Print()
        {
            Console.WriteLine(string.Join(" ", _stones));
        }

        public void PrintNumber()
        {
            Console.WriteLine(_stones.Count);
        }
    }
}
