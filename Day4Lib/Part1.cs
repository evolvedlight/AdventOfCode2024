
namespace Day4Lib
{
    public class Part1
    {
        public static int GetXmases(char[][] input)
        {
            // Loop through the array checking for X
            var countXes = 0;
            for (int y = 0; y < input.Length; y++)
            {
                for (int x = 0; x < input[y].Length; x++)
                {
                    if (input[y][x] == 'X')
                    {
                        countXes += countXMasFromHere(input, x, y);
                    }
                }
            }

            return countXes;
        }

        private static int countXMasFromHere(char[][] input, int x, int y)
        {
            var xmasesHere = 0;
            
            for (int changeX = -1; changeX <= 1; changeX++)
            {
                for (int changeY = -1; changeY <= 1; changeY++)
                {
                    var isXmas = IsXmasHereInThisDirection(input, x, y, changeX, changeY);
                    if (isXmas)
                    {
                        xmasesHere++;
                    }
                }
            }
            return xmasesHere;
        }

        private static bool IsXmasHereInThisDirection(char[][] input, int x, int y, int changeX, int changeY)
        {
            var lettersNeeded = "XMAS".ToCharArray();
            for (int charIndex = 0; charIndex < lettersNeeded.Length; charIndex++)
            {
                var newX = x + changeX * charIndex;
                var newY = y + changeY * charIndex;

                if (newX < 0 || newY < 0 || newX >= input[0].Length || newY >= input.Length)
                {
                    return false;
                }

                var testedChar = input[newY][newX];

                if (testedChar != lettersNeeded[charIndex])
                {
                    return false;
                }
            }
            return true;
        }
    }
}
