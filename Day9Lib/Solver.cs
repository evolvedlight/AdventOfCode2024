
using System.Text;

namespace Day9Lib
{
    public class Solver
    {
        public static long SolvePart1(string input)
        {
            // build a lookup from the end to the start
            List<TailItem> tailLookup = [];

            var text = new StringBuilder();

            var count = 0;
            foreach (var character in input.ToCharArray()) {
                var numberForm = int.Parse(character.ToString());

                if (count % 2 == 0)
                {
                    tailLookup.Add(new TailItem(count / 2, numberForm));
                }
                count++;
            }

            tailLookup.Reverse();

            var originalFileIndexId = 0;
            var blockPosition = 0;
            foreach (var character in input.ToCharArray())
            {
                if (originalFileIndexId + 1 + tailLookup[0].index >= count)
                {
                    var nextTail = tailLookup[0];
                    for (var i = 0; i < nextTail.remainingChars; i++)
                    {
                        text.Append(nextTail.index);
                        blockPosition++;
                    }
                    Console.WriteLine(text.ToString());

                    var result = text.ToString().Select((character, index) => index * long.Parse(character.ToString())).Sum();

                    return result;
                }

                var numberForm = int.Parse(character.ToString());
                if (originalFileIndexId % 2 == 0)
                {
                    blockPosition += numberForm;
                    text.Append(string.Join("", Enumerable.Repeat(originalFileIndexId / 2, numberForm)));
                }
                else
                {
                    // we strip some numbers from the tail
                    while (numberForm > 0)
                    {
                        var nextTail = tailLookup[0];

                        // if numberForm is 2 and we have 3, we want to change it to 1
                        // if numberform is 3 and we have 2, we want to change it to 0 and leave numberform at 1
                        if (numberForm >= nextTail.remainingChars)
                        {
                            // This is the case where we take all of the things we can, pop the tail
                            for (var i = 0; i < nextTail.remainingChars; i++)
                            {
                                text.Append(nextTail.index);
                                blockPosition++;
                            }

                            numberForm = numberForm - nextTail.remainingChars;
                            tailLookup = tailLookup.Skip(1).ToList();
                        }
                        else
                        {
                            // This is where we just use some stuff from the tail but not all
                            for (var i = 0; i < numberForm; i++)
                            {
                                text.Append(nextTail.index);
                                blockPosition++;
                            }
                            nextTail.remainingChars -= numberForm;
                            numberForm = 0;
                        }
                    }
                }

                originalFileIndexId++;
            }

            throw new Exception("ooh no");
        }
    }

    public class TailItem
    {
        public int index;
        public int remainingChars;

        public TailItem(int index, int remainingChars)
        {
            this.index = index;
            this.remainingChars = remainingChars;
        }
    }
}
