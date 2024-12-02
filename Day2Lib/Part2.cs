using System.Windows.Markup;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Day2Lib
{
    public class Part2
    {
        public static bool IsSafeBruteForce(List<int> records)
        {
            if (Part1.IsSafe(records))
            {
                return true;
            }
            for (int i = 0; i <= records.Count; i++)
            {
                var recordsPart = records[..i].Concat(records.Skip(i + 1).ToList());

                if (Part1.IsSafe(recordsPart))
                {
                    return true;
                }
            }
            return false;
        }

        public static bool IsSafe(List<int> d)
        {
            static bool InternalIsSafe(List<int> d, bool tolerateErrors = false)
            {
                for (int i = 0; i < d.Count - 1; i++)
                {
                    int difference = d[i] - d[i + 1];
                    bool isWithinRange = 1 <= difference && difference <= 3;

                    if (!isWithinRange)
                    {
                        return tolerateErrors && Enumerable.Range(i, 2).Any(j => InternalIsSafe(d.Take(j).Concat(d.Skip(j + 1)).ToList()));
                    }
                }
                return true;
            }
            return InternalIsSafe(d, true) || InternalIsSafe(d.AsEnumerable().Reverse().ToList(), true);
        }
    }
}