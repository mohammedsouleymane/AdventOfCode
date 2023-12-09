namespace Aoc2023;

public static class Day09
{
    private static readonly List<string> Lines = Util.ReadFile("/day09/input");


    private static int Extrapolate(int[] numbers, int part = 1)
    {
        if (numbers.All(x => x == 0))
            return 0;
        var nn = new int[numbers.Length - 1];
        for (var i = 0; i < numbers.Length - 1; i++)
            nn[i] = numbers[i + 1] - numbers[i];

        if (part == 1)
            return numbers[^1] + Extrapolate(nn); // part one
        return numbers.First() - Extrapolate(nn,2); // part two
    }
    
    public static int One => Lines.Sum(line => Extrapolate(line.Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray()));
    public static int Two => Lines.Sum(line => Extrapolate(line.Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray(),2));
    
}