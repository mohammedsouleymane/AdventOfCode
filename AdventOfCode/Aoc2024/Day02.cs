namespace Aoc2024;

public static class Day02
{
    private static readonly List<string> Reports = Util.ReadFile("/day02/input");
    public static readonly int SafeReports = Reports.Count(x => Increasing(x) || Decreasing(x));
    public static readonly int SafeReportsWithDampener = Reports.Count(Dampener);

    private static bool Dampener(string report)
    {
        if (Increasing(report) || Decreasing(report))
            return true;
        for (var i = 0; i < report.Split(" ").Length; i++)
        {
            var levels = report.Split(" ").ToList();
            levels.RemoveAt(i);
            if (Increasing(levels.ToStr(" ")) || Decreasing(levels.ToStr(" ")))
                return true;
        }
        return false;
    }
    private static bool Increasing(string report)
    {
        var levels = report.Split(" ").Select(int.Parse).ToArray();
        for (var i = 1; i < levels.Length; i++)
        {
            if(levels[i] > levels[i-1] && Math.Abs(levels[i] - levels[i-1]) < 4)
                continue;
            return false;
        }
        return true;
    }

    private static bool Decreasing(string report)
    {
        var levels = report.Split(" ").Select(int.Parse).ToArray();    
        for (var i = 1; i < levels.Length; i++)
        {
            if(levels[i] < levels[i-1] && Math.Abs(levels[i] - levels[i-1]) < 4)
                continue;
            return false;
        }
        return true;
    }

}