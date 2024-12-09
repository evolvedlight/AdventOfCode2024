namespace Day9Tests
{
    public class Part1Tests
    {
        [Fact]
        public void ExampleTest()
        {
            var input = "2333133121414131402";

            var result = Day9Lib.Solver.SolvePart1(input);

            Assert.Equal(1928, result);
        }

        [Fact]
        public void ExampleTest2()
        {
            var input = "2222";

            var result = Day9Lib.Solver.SolvePart1(input);

            Assert.Equal(7, result);
        }
    }
}
