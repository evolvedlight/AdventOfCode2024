using System.Windows.Markup;

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
                var recordsPart = records[..i].Concat(records.Skip(i+1).ToList());

                if (Part1.IsSafe(recordsPart))
                {
                    return true;
                }
            }
            return false;
        }

        public static bool IsSafe(List<int> records)
        {

            var (isSafe, unsafeIndex) = IsSafeLevels(records);

            if (isSafe)
            {
                return true;
            }

            for (var i = unsafeIndex > 0 ? unsafeIndex - 1 : unsafeIndex; i <= unsafeIndex; i++)
            {
                (isSafe, _) = IsSafeLevels([.. records[..i], .. records[(i + 1)..]]);
                if (isSafe)
                {
                    return true;
                }
            }

            return false;
        }

        static (bool isSafe, int unsafeIndex) IsSafeLevels(List<int> records)
        {
            var (increasing, isSafeDiff) = Change(records[0], records[1]);

            if (!isSafeDiff)
            {
                return (false, 0);
            }

            for (var i = 1; i < records.Count; i++)
            {
                var (increase, safeDiff) = Change(records[i], records[i+1]);
                if (!isSafeDiff || increase != increasing)
                {
                    return (false, i);
                }
            }

            return (true, -1);
        }

        static (bool increasing, bool isSafeDiff) Change(int a, int b)
        {
            var diff = b - a;
            var increase = Math.Sign(diff) == 1;
            var amount = Math.Abs(diff);
            return (increase, amount is 1 or 2 or 3);
        }
    }
}
