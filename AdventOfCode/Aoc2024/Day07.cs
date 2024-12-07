namespace Aoc2024;

public static class Day07
{
    private static readonly List<string> Equations = Util.ReadFile("/day07/input");


    public static long Calibrate(bool two = false)
    {
        long sum = 0;
        foreach (var equation in Equations)
        {
            var split = equation.Split(": ");
            var answer = long.Parse(split[0]);
            var numbers = split[1].Split(" ").Select(long.Parse).ToList();
            if (Valid(numbers.Skip(1).ToList(), answer, numbers.First()))
                sum += answer;
        }

        return sum;
    }

    private static bool Valid(List<long> numbers, long answer, long current)
    {
        if (current > answer)
            return false;
        if (numbers.Count == 0)
            return current == answer;
        var first = numbers.First();
        var rest = numbers.Skip(1).ToList();
        return
            Valid(rest, answer, current + first) ||
            Valid(rest, answer, current * first) ||
            Valid(rest, answer, long.Parse($"{current}{first}")); //part two
    }
}