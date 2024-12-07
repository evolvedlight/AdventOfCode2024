
using System.Linq.Expressions;

namespace Day7Lib
{
    public class Solver
    {
        public static long GetPossibleWaysSum(string[] fileLines)
        {
            long sum = 0;
            foreach (var line in fileLines)
            {
                
                var total = long.Parse(line.Split(":").First());
                var parts = line.Split(':').Last().Split(" ").Where(s => !string.IsNullOrWhiteSpace(s)).Select(s => int.Parse(s));
               
                if (IsPossible(total, parts))
                {
                    sum += total;
                }
            }

            return sum;
        }

        public static long GetPossibleWaysSum2(string[] fileLines)
        {
            long sum = 0;
            foreach (var line in fileLines)
            {
                var total = long.Parse(line.Split(":").First());
                var parts = line.Split(':').Last().Split(" ").Where(s => !string.IsNullOrWhiteSpace(s)).Select(s => int.Parse(s));

                if (IsPossible2(total, parts.ToList()))
                {
                    sum += total;
                }
            }

            return sum;
        }

        public static bool IsPossible(long total, IEnumerable<int> remainingNumbers)
        {
            if (!remainingNumbers.Any())
            {
                return total == 0;
            }
            var lastNumber = remainingNumbers.Last();
            var possible = false;
            if (total % lastNumber == 0)
            {
                if (IsPossible(total / lastNumber, remainingNumbers.Take(remainingNumbers.Count() - 1)))
                {
                    possible = true;
                }
            }

            if (IsPossible(total - lastNumber, remainingNumbers.Take(remainingNumbers.Count() - 1)))
            {
                possible = true;
            }

            return possible;
        }

        public static bool IsPossible2(long total, List<int> remainingNumbers)
        {
            if (total < 0)
            {
                // reduce logging
                return false;
            }

            if (remainingNumbers.Count == 1 && remainingNumbers[0] == total)
            {
                return true;
            }
            if (remainingNumbers.Count == 0)
            {
                return total == 0;
            }
            var lastNumber = remainingNumbers.Last();

            // if the total ends with the current last number
            return TryConcats(total, remainingNumbers) || TryMultiplication(total, remainingNumbers, lastNumber) || TryAddition(total, remainingNumbers, lastNumber);
        }

        private static bool TryAddition(long total, List<int> remainingNumbers, int lastNumber)
        {
            return IsPossible2(total - lastNumber, remainingNumbers.Take(remainingNumbers.Count - 1).ToList());
        }

        private static bool TryMultiplication(long total, List<int> remainingNumbers, int lastNumber)
        {
            if (total % lastNumber == 0)
            {
                if (total == 0 && remainingNumbers.Any())
                {
                    return false;
                }
                return (IsPossible2(total / lastNumber, remainingNumbers.Take(remainingNumbers.Count - 1).ToList()));
            }
            return false;
        }

        private static bool TryConcats(long total, List<int> remainingNumbers)
        {
            if (total.ToString().EndsWith(remainingNumbers.Last().ToString()))
            {
                var chars = remainingNumbers.Last().ToString().Count();
                // we try a concat
                return (IsPossible2((long)(total / Math.Pow(10, chars)), remainingNumbers.Take(remainingNumbers.Count - 1).ToList()));
            }
            return false;
        }
    }
}
