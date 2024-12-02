namespace Day2Lib
{
    public class Part1
    {
        public static bool IsSafe(IEnumerable<int> records)
        {
            var diffs = records.Zip(records.Skip(1)).Select((numbers) => numbers.First - numbers.Second);
            var signsSame = diffs.Select(Math.Sign).Distinct().Count() == 1;
            var rangesOk = diffs.All(x => Math.Abs(x) <= 3 && Math.Abs(x) > 0);

            return signsSame && rangesOk;
        }
    }
}
