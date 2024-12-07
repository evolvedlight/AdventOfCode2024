namespace Day7Tests
{
    public class Part2Tests
    {
        [Fact]
        public void ExampleLine1()
        {
            var result = Day7Lib.Solver.IsPossible2(190, [10, 19]);

            Assert.True(result);
        }

        [Fact]
        public void ExampleLine2()
        {
            var result = Day7Lib.Solver.IsPossible2(3267, [81, 40, 27]);

            Assert.True(result);
        }

        [Fact]
        public void ExampleLine3()
        {
            var result = Day7Lib.Solver.IsPossible2(83, [17, 5]);

            Assert.False(result);
        }

        [Fact]
        public void ExampleLineLast()
        {
            //292: 11 6 16 20
            var result = Day7Lib.Solver.IsPossible2(292, [11, 6, 16, 20]);

            Assert.True(result);
        }

        [Fact]
        public void ExampleP2_1()
        {
            //292: 11 6 16 20
            var result = Day7Lib.Solver.IsPossible2(156, [15, 6]);

            Assert.True(result);
        }

        [Fact]
        public void ExampleP2_2()
        {
            //292: 11 6 16 20
            var result = Day7Lib.Solver.IsPossible2(7290, [6, 8, 6, 15]);

            Assert.True(result);
        }

        [Fact]
        public void ExampleP2_3()
        {
            //292: 11 6 16 20
            var result = Day7Lib.Solver.IsPossible2(192, [17, 8, 14]);

            Assert.True(result);
        }

        [Fact]
        public void ExampleP2_SB()
        {
            var result = Day7Lib.Solver.IsPossible2(486, [6, 8, 6]);

            Assert.True(result);
        }
        
        [Fact]
        public void ExampleP2_SB2()
        {
            var result = Day7Lib.Solver.IsPossible2(2170366, [42, 3, 49, 6, 6, 7, 1, 6, 3, 6, 8, 8]);

            Assert.False(result);
        }
    }
}
