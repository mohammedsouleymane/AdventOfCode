namespace Aoc2023;

public static class Day08
{
    private static readonly string[] Network = Util.ReadFile("/day08/input").ToArray();
    private static readonly List<int> Instructions = Network.First().Select(c => c == 'L' ? 0 : 1).ToList();

    public static int One()
    {
        var dict = new Dictionary<string, string[]>();
        for (var i = 2; i < Network.Length; i++)
        {
            var line = Network[i].Split(" = ", StringSplitOptions.RemoveEmptyEntries);
            var mapping = line[1].Replace("(", "").Replace(")", "").Split(", ", StringSplitOptions.RemoveEmptyEntries);
            dict.Add(line[0], mapping);
        }

        var count = 0;
        var node = "AAA";
        while (node != "ZZZ")
            node = dict[node][Instructions[count++ % Instructions.Count]];

        return count;
    }

    private static long CalculateLeastCommonMultiple(long a, long b)
    {
        var max = Math.Max(a, b);
        var min = Math.Min(a, b);
        for (long i = 1; i <= min; i++)
        {
            if (max * i % min == 0)
            {
                return i * max;
            }
        }

        return min;
    }

    public static long Two()
    {
        var startingNodes = new List<string>();
        var dict = new Dictionary<string, string[]>();
        for (var i = 2; i < Network.Length; i++)
        {
            var line = Network[i].Split(" = ", StringSplitOptions.RemoveEmptyEntries);
            var mapping = line[1].Replace("(", "").Replace(")", "").Split(", ", StringSplitOptions.RemoveEmptyEntries);
            if (line[0].EndsWith("A")) startingNodes.Add(line[0]);
            dict.Add(line[0], mapping);
        }

        long lcm = 1;
        foreach (var node in startingNodes)
        {
            var n = node;
            var count = 0;
            while (!n.EndsWith("Z"))
                n = dict[n][Instructions[count++ % Instructions.Count]];
            lcm = CalculateLeastCommonMultiple(lcm, count);
        }
        return lcm;
    }
}