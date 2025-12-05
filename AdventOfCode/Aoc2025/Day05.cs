namespace Aoc2025;

public static class Day05
{
    private static readonly List<List<string>> Input = Util.ReadFile("/day05/input").SplitToChucks("");

    public static long Fresh()
    {
        var ranges = Input.First().Select(x=> x.Split("-")).Select(x=> (long.Parse(x[0]), long.Parse(x[1]))).ToArray();
        var ingredients = Input.Last().Select(long.Parse);
        return ingredients.Count(ingredient => ranges.Any(x => x.Item1 <= ingredient && x.Item2 >= ingredient));
    }

    public static long ConsideredIds()
    {
        var ranges = Input.First().Select(x=> x.Split("-")).Select(x=> (long.Parse(x[0]) , long.Parse(x[1])) ).OrderBy(x=> x.Item1).ThenBy(x=> x.Item2);
        long sum = 0;

        List<(long, long)> doneRanges = [];
        foreach (var (l,r) in ranges)
        {
            if(doneRanges.Any(x=>  x.Item2 >= r )) // ignore because it's fully in another range (10 - 14 contains 12-13)
                continue;
            var overlap = doneRanges.Where(x => x.Item2 >= l).ToList(); // finds overlapping ranges
            if (overlap.Count != 0)
                sum += r - overlap.Last().Item2 ;
            else
                sum += r - l + 1;
            doneRanges.Add((l,r));
        }

        return sum;
    }
}