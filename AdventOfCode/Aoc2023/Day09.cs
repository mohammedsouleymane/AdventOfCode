namespace Aoc2023;

public static class Day09
{
    private static readonly List<string> Lines = Util.ReadFile("/day09/input");


    private static int Extrapolate(IReadOnlyList<int> numbers, int part = 1)
    {
        if (numbers.All(x => x == 0))
            return 0;
        var differences = numbers.Skip(1).Select((x, i) => x - numbers[i]).ToArray();
        if (part == 1)
            return numbers[^1] + Extrapolate(differences); // part one
        return numbers[0] - Extrapolate(differences,2); // part two
    }
    
    public static int One => Lines.Sum(line => Extrapolate(line.Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray()));
    public static int Two => Lines.Sum(line => Extrapolate(line.Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray(),2));
    
}