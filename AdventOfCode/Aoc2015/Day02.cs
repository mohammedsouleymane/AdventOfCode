namespace Aoc2015;

public static class Day02
{
    private static readonly List<int[]> Dimensions = Util.ReadFile("/day02/test").Select(x=> x.Split("x").Select(int.Parse).ToArray()).ToList();

    public static int Surface()
    {
        var sum = 0;   
        foreach (var dimension in Dimensions)
        {
            int[] l = [dimension[0] * dimension[1], dimension[1] * dimension[2], dimension[0] * dimension[2]];
            sum += l.Sum(x => 2 * x) + l.Min();
        }
        return sum;
    }

    public static int Ribbon()
    {
        var sum = 0;   
        foreach (var dimension in Dimensions)
        {
            var order = dimension.Order().ToArray().Take(2);
            sum += dimension.Aggregate(1, (x, y) => x * y) + order.Sum(x=> 2*x);
        }
        return sum;
    }
}