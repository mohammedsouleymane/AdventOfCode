namespace Aoc2023;

public static class Day06
{
    private static readonly List<string> Inputs = Util.ReadFile("/day06/input");

    // can also use quadratic equation = -x^2 - time*x - record
    private static long HowManyWays(long record,  long time)
    {
        var firstTimeToBeatRecord = 0;
        for (var j = 0; j <= time; j++)
        {
            if ((time - j) * j > record)
            {
                firstTimeToBeatRecord = j; // returns first time that beats record
                break;
            };
        }
        return time - 2 * firstTimeToBeatRecord + 1; // first time to beat the record = the amount that gets skipped in the beginning and end of your time
    }

    public static long One()
    {
        var times = Inputs.First().Split(": ")[1]
            .Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(long.Parse).ToArray();

        var distances = Inputs.Last().Split(": ")[1]
            .Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(long.Parse).ToArray();

        return times.Select((t, i) => HowManyWays(distances[i], t)).Aggregate<long, long>(1, (current, b) => current * b);
    }
    
    public static long Two()
    {
        var time = long.Parse(Inputs.First().Split(": ")[1].Replace(" ",""));
        var distance = long.Parse(Inputs.Last().Split(": ")[1].Replace(" ", ""));
        return HowManyWays( distance,time);
    }
}
