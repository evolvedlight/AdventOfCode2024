namespace Day5Lib
{
    public class Parser
    {
        public static RulesAndPages Parse(List<string> input)
        {
            var result = new RulesAndPages();
            
            foreach (var line in input)
            {
                if (line.Contains("|"))
                {
                    var parts = line.Split('|');
                    result.Rules.Add((int.Parse(parts[0]), int.Parse(parts[1])));
                }
                else if (!string.IsNullOrEmpty(line))
                {
                    result.Updates.Add(line.Split(",").Select(int.Parse).ToList());
                }
            }

            return result;
        }
    }

    public class RulesAndPages
    {
        public List<(int, int)> Rules { get; set; } = [];
        public List<List<int>> Updates { get; set; } = [];
    }
}
