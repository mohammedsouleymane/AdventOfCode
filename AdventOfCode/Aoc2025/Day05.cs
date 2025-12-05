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
        var ranges = Input.First()
            .Select(x=> x.Split("-"))
            .Select(x=> (long.Parse(x[0]) , long.Parse(x[1])) )
            .OrderBy(x=> x.Item1).ThenBy(x=> x.Item2)
            .ToList();// sorts on start and then end of the ranges
        var sum = ranges.First().Item2 - ranges.First().Item1 + 1; // initial range
        var endOfBefore = ranges.First().Item2; 
        
        foreach (var (l,r) in ranges.Skip(1))
        {
            if(r <= endOfBefore) continue; // if the right hand is smaller or equal that means the ranges before fully contains the current one (10 - 14) contains (11 - 13)
            if (l > endOfBefore) // if left hand if bigger than the ending range of before we just subtract r from l + 1
                sum += r - l + 1;
            else
                sum += r - endOfBefore; // otherwise r - end of the range before

            endOfBefore = r;
        } // this all works because of the sorting

        return sum;
    }
}