using System.Text.RegularExpressions;

namespace Aoc2024;

public static class Day03
{
    private static readonly List<string> Corrupted = Util.ReadFile("/day03/input");

    public static int SolveMultiplications(bool two = false)
    {
        var sum = 0;
        var curr = "do()";
        foreach (var corrupt in Corrupted)
        {
            var patterns = Regex.Matches(corrupt, @"mul\((\d+),\s*(\d+)\)|do\(\)|don't\(\)"); // do and dont pattern for part 2
            foreach (var pattern in patterns.Select(m => m.Value).ToArray())
            {
                if (two)
                    curr = pattern.StartsWith("do") ? pattern : curr; 
                if (curr != "do()" || !pattern.StartsWith("mul")) continue;
                sum += pattern[4..^1].Split(",").Select(int.Parse).Aggregate((x,y) => x* y);
            }
        }
        return sum;
    }
}
