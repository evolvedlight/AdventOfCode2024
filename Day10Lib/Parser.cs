using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day10Lib;

public class Parser
{
    public static int[][] ParseMap(IEnumerable<string> inputFiles)
    {
        return inputFiles.Select(x => x.Select(x => char.IsDigit(x) ? int.Parse(x.ToString()) : -2).ToArray()).ToArray();
    }
}
