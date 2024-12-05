using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day5Lib;

public class Solver
{
    public static int Solve1P(RulesAndPages input)
    {
        return input.Updates.Select(x => CheckUpdate(input.Rules, x)).Where(x => x.middleNumber.HasValue).Select(x => x.middleNumber!.Value).Sum();
    }

    private static (bool isCorrect, int? middleNumber) CheckUpdate(List<(int, int)> rules, List<int> update)
    {
        // first get all rules where both numbers are there
        var possibleRules = rules.Where(x => update.Contains(x.Item1) && update.Contains(x.Item2));
        foreach (var rule in possibleRules)
        {            
            var ruleTarget = rule.Item2;

            // page number Item1 must be printed before this number

            var indexOfNumber = update.IndexOf(ruleTarget);

            var numbersBeforeItem2 = update[..indexOfNumber];

            if (!numbersBeforeItem2.Contains(rule.Item1))
            {
                return (false, null);
            }
        }

        return (true, update[update.Count / 2]);
    }

    public static int SolveP2(RulesAndPages input)
    {
        return input.Updates.Select(x => GetSortedMiddle(input.Rules, x)).Sum();
    }

    public static int GetSortedMiddle(List<(int, int)> rules, List<int> update)
    {
        if (CheckUpdate(rules, update).isCorrect)
        {
            return 0;
        }

        var possibleRules = rules.Where(x => update.Contains(x.Item1) && update.Contains(x.Item2)).OrderBy(x => x.Item1);
        foreach (var rule in possibleRules)
        {
            var ruleTarget = rule.Item2;

            // page number Item1 must be printed before this number

            var indexOfNumber = update.IndexOf(ruleTarget);

            var numbersBeforeItem2 = update[..indexOfNumber];

            if (!numbersBeforeItem2.Contains(rule.Item1))
            {
                // we now need to correct this by moving Item1 infront of Item2
                update.Remove(rule.Item1);
                update.Insert(indexOfNumber, rule.Item1);
            }
        }
        var ans = update[update.Count / 2];
        return ans;
    }
}
