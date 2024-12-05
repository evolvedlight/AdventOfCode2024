
namespace Day4Lib
{
    public class Part2
    {
        public static int GetMasXes(char[][] input)
        {
            // Loop through the array checking for A
            var countMas = 0;
            for (int y = 0; y < input.Length; y++)
            {
                for (int x = 0; x < input[y].Length; x++)
                {
                    if (input[y][x] == 'A')
                    {
                        if (isMasHere(input, x, y))
                        {
                            countMas++;
                        }
                        
                    }
                }
            }

            return countMas;
        }

        private static bool isMasHere(char[][] input, int x, int y)
        {
            int xmasesHere = 0;
            
            for (int changeX = -1; changeX <= 1; changeX += 2)
            {
                for (int changeY = -1; changeY <= 1; changeY += 2)
                {
                    var isXmas = IsMasHereInThisDirection(input, x, y, changeX, changeY);
                    if (isXmas)
                    {
                        xmasesHere++;
                    }
                }
            }
            return xmasesHere == 2;
        }

        private static bool IsMasHereInThisDirection(char[][] input, int x, int y, int changeX, int changeY)
        {
            var lettersNeeded = "MAS".ToCharArray();
            for (int charIndex = 0; charIndex < lettersNeeded.Length; charIndex++)
            {
                var newX = x + changeX * (charIndex - 1);
                var newY = y + changeY * (charIndex - 1);

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
