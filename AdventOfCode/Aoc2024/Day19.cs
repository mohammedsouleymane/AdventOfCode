namespace Aoc2024;

public static class Day19
{
    private static readonly List<string> Input = Util.ReadFile("/day19/input");
    private static readonly List<string> Towels = Input.First().Split(", ").ToList();
    private static readonly List<string> Designs = Input.Skip(2).ToList();

    private static readonly Dictionary<string, bool> Cache = new();
    
    private static bool IsPossible(string design)
    {
        if (design == "") return true;
        if (Cache.TryGetValue(design, out var possible)) return possible;
        foreach (var towel in Towels)
        {
            if (design.StartsWith(towel) && IsPossible(design[towel.Length..]))
            {
                Cache[design] = true;
                return true;
            }
        }

        Cache[design] = false;
        return false;
    }
    private static readonly Dictionary<string, long> Count = new();
    private static long CountPossibility(string design)
    {
        if (design == "") return 1;
        if (Count.TryGetValue(design, out var count)) return count ;
        foreach (var towel in Towels)
        {
            if (design.StartsWith(towel))
            {
                count += CountPossibility(design[towel.Length..]);
            }
        }
        Count[design] = count;
        return count;
    }
    public static int PossibleDesigns => Designs.Count(IsPossible);
    public static long CountPossibilities => Designs.Sum(CountPossibility);
   
}