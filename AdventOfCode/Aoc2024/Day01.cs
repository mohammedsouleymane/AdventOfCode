namespace Aoc2024;

public static  class Day01
{
    private static readonly List<string> Pairs = Util.ReadFile("/day01/input");
    private static readonly List<int> Left = Pairs.Select(x => int.Parse(x.Split("  ")[0].Trim())).Order().ToList();
    private static readonly List<int> Right = Pairs.Select(x => int.Parse(x.Split("  ")[1].Trim())).Order().ToList();
    public static readonly int Difference = Left.Zip(Right).Sum( x => Math.Abs(x.First - x.Second));
    public static readonly int SimilarityScore = Left.Sum(x => Right.Count(y => x == y) * x);



}
