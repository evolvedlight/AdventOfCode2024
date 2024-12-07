namespace Day7Tests
{
    public class Part1Tests
    {
        [Fact]
        public void ExampleLine1()
        {
            var result = Day7Lib.Solver.IsPossible(190, [10, 19]);

            Assert.True(result);
        }
        
        [Fact]
        public void ExampleLine2()
        {
            var result = Day7Lib.Solver.IsPossible(3267, [81, 40, 27]);

            Assert.True(result);
        }
        
        [Fact]
        public void ExampleLine3()
        {
            var result = Day7Lib.Solver.IsPossible(83, [17, 5]);

            Assert.False(result);
        }
        
        [Fact]
        public void ExampleLineLast()
        {
            //292: 11 6 16 20
            var result = Day7Lib.Solver.IsPossible(292, [11, 6, 16, 20]);

            Assert.True(result);
        }
    }
}
