using System.Text.RegularExpressions;

namespace Day3Lib
{
    public partial class Part2
    {
        public int Solve(string input)
        {
            // we'll now put the sequences together and look for the "last" one before this number
            List<(int position, bool isEnabled)> enabledOrNot = [];
            enabledOrNot.Add((0, true));
            // first find all the dos and don'ts and their positions
            var dontRegex = DontRegex();
            enabledOrNot.AddRange(dontRegex.Matches(input).Select(x => (x.Index, false)));

            var doRegex = DoRegex();
            enabledOrNot.AddRange(doRegex.Matches(input).Select(x => (x.Index, true)));
            
            var sortedEnabledOrNot = enabledOrNot.OrderBy(x => x.position).ToList();

            var mulRegex = MulRegex();

            var matches = mulRegex.Matches(input);
            var sum = 0;
            foreach (Match m in matches)
            {
                var lastSwitch = sortedEnabledOrNot.Where(x => x.position < m.Index).Last();
                if (lastSwitch.isEnabled)
                {
                    sum += int.Parse(m.Groups[1].Value) * int.Parse(m.Groups[2].Value);
                }
                
            }

            return sum;
        }

        [GeneratedRegex("mul\\((?'first'\\d+),(?'second'\\d+)\\)")]
        private static partial Regex MulRegex();

        [GeneratedRegex("do\\(\\)")]
        private static partial Regex DoRegex();

        [GeneratedRegex("don\\'t\\(\\)")]
        private static partial Regex DontRegex();

        public int SolveWithBigRegex(string input)
        {
            var bigRegex = BigRegex();

            var matches = bigRegex.Matches(input);
            var sum = 0;
            var enabled = true;
            foreach (Match m in matches)
            {
                if (m.Value == "don't()")
                {
                    enabled = false;
                    continue;
                }
                if (m.Value == "do()")
                {
                    enabled = true;
                    continue;
                }
                if (enabled)
                {
                    sum += int.Parse(m.Groups["first"].Value) * int.Parse(m.Groups["second"].Value);
                }
                
            }

            return sum;
        }

        [GeneratedRegex("mul\\((?'first'\\d+),(?'second'\\d+)\\)|(do\\(\\))|(don't\\(\\))")]
        private static partial Regex BigRegex();
    }
}
