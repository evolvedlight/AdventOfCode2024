using Day2Lib;

namespace Day2Tests
{
    public class Day2Part2Tests
    {
        [Fact]
        public void Day2Tests_1()
        {
            List<int> report = [7, 6, 4, 2, 1];

            Assert.True(Part2.IsSafeBruteForce(report));

            Assert.True(Part2.IsSafe(report));
        }

        [Fact]
        public void Day2Tests_2()
        {
            List<int> report = [1, 2, 7, 8, 9];

            Assert.False(Part2.IsSafeBruteForce(report));

            Assert.False(Part2.IsSafe(report));
        }

        [Fact]
        public void Day2Tests_3()
        {
            List<int> report = [9, 7, 6, 2, 1];

            Assert.False(Part2.IsSafeBruteForce(report));
            Assert.False(Part2.IsSafe(report));
        }

        [Fact]
        public void Day2Tests_4()
        {
            List<int> report = [1, 3, 2, 4, 5];

            Assert.True(Part2.IsSafeBruteForce(report));
            Assert.True(Part2.IsSafe(report));
        }

        [Fact]
        public void Day2Tests_5()
        {
            List<int> report = [8, 6, 4, 4, 1];

            Assert.True(Part2.IsSafeBruteForce(report));
            Assert.True(Part2.IsSafe(report));
        }

        [Fact]
        public void Day2Tests_6()
        {
            List<int> report = [1, 3, 6, 7, 9];

            Assert.True(Part2.IsSafeBruteForce(report));
            Assert.True(Part2.IsSafe(report));
        }

        [Fact]
        public void Day2Tests_SB()
        {
            List<int> report = [1, 3, 6, 7, 17];

            Assert.True(Part2.IsSafeBruteForce(report));
            Assert.True(Part2.IsSafe(report));
        }

        [Fact]
        public void Day2Tests_SB2()
        {
            List<int> report = [1, 3, 6, 7, 17, 31];

            Assert.False(Part2.IsSafeBruteForce(report));
            Assert.False(Part2.IsSafe(report));
        }
        
        [Fact]
        public void Day2Tests_SB3()
        {
            List<int> report = [43, 46, 48, 49, 52, 49, 52, 49];

            Assert.False(Part2.IsSafeBruteForce(report));
            Assert.False(Part2.IsSafe(report));
        }
        
        [Fact]
        public void Day2Tests_SB4()
        {
            List<int> report = [87, 85, 84, 81, 79, 82];

            Assert.True(Part2.IsSafeBruteForce(report));
            Assert.True(Part2.IsSafe(report));
            
        }
    }
}
