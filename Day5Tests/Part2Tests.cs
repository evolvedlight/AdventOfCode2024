using Day5Lib;

namespace Day5Tests
{
    public class Part2Tests
    {
        [Fact]
        public void CheckOrderedMiddleCorrect()
        {
            List<int> input = [75, 47, 61, 53, 29];
            var rules = new List<(int, int)>
            {
               (47,53),
               (97,13),
               (97,61),
               (97,47),
               (75,29),
               (61,13),
               (75,53),
               (29,13),
               (97,29),
               (53,29),
               (61,53),
               (97,53),
               (61,29),
               (47,13),
               (75,47),
               (97,75),
               (47,61),
               (75,61),
               (47,29),
               (75,13),
               (5,13)
            };

            var ordered = Solver.GetSortedMiddle(rules, input);

            Assert.Equal(0, ordered);
        }

        [Fact]
        public void CheckOrderedMiddleEx1()
        {
            List<int> input = [75,97,47,61,53];
            var rules = new List<(int, int)>
            {
               (47,53),
               (97,13),
               (97,61),
               (97,47),
               (75,29),
               (61,13),
               (75,53),
               (29,13),
               (97,29),
               (53,29),
               (61,53),
               (97,53),
               (61,29),
               (47,13),
               (75,47),
               (97,75),
               (47,61),
               (75,61),
               (47,29),
               (75,13),
               (5,13)
            };

            var ordered = Solver.GetSortedMiddle(rules, input);

            Assert.Equal(47, ordered);
        }
    }
}
