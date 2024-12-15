using System.Text.RegularExpressions;

namespace Day13Lib
{
    public partial class ArcadeMachine
    {
        public int ButtonAX { get; set; }
        public int ButtonAY { get; set; }
        public int ButtonBX { get; set; }
        public int ButtonBY { get; set; }
        public int PrizeX { get; set; }
        public int PrizeY { get; set; }

        public static ArcadeMachine FromLines(string[] lines)
        {
            var arcadeMachine = new ArcadeMachine();

            var buttonRegex = ButtonRegex();

            var aMatch = buttonRegex.Match(lines[0]);
            arcadeMachine.ButtonAX = int.Parse(aMatch.Groups["xInt"].Value);
            arcadeMachine.ButtonAY = int.Parse(aMatch.Groups["yInt"].Value);

            var bMatch = buttonRegex.Match(lines[1]);
            arcadeMachine.ButtonBX = int.Parse(bMatch.Groups["xInt"].Value);
            arcadeMachine.ButtonBY = int.Parse(bMatch.Groups["yInt"].Value);

            var prizeRegex = PrizeRegex();
            var prizeMatch = prizeRegex.Match(lines[2]);
            arcadeMachine.PrizeX = int.Parse(prizeMatch.Groups["xPrize"].Value);
            arcadeMachine.PrizeY = int.Parse(prizeMatch.Groups["yPrize"].Value);
            return arcadeMachine;
        }

        [GeneratedRegex("Button (?'letter'\\w): (\\w)\\+(?'xInt'[\\d]+), (\\w)\\+(?'yInt'[\\d]+)")]
        private static partial Regex ButtonRegex();

        [GeneratedRegex("Prize: (\\w)\\=(?'xPrize'[\\d]+), (\\w)\\=(?'yPrize'[\\d]+)")]
        private static partial Regex PrizeRegex();
    }

    public static class MathsMethods
    {
        public static (bool isPossible, double? a, double? b) CramersRule(long a1, long a2, long b1, long b2, long c1, long c2)
        {
            double determinantA = (a1 * b2) - (a2 * b1);

            if (determinantA == 0)
            {
                Console.WriteLine("No solution");
                return (false, null, null);
            }

            double determinantA_a = (c1 * b2) - (c2 * b1);
            double determinantA_b = (a1 * c2) - (a2 * c1);

            // cramers rule
            double a = determinantA_a / determinantA;
            double b = determinantA_b / determinantA;

            return (true, a, b);
        }
    }
}
