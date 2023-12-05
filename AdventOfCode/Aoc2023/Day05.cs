using System.Collections;

namespace Aoc2023;

public static class Day05
{
    private static readonly List<string> Almanac = Util.ReadFile("/day05/input");

    public static long One()
    {
        var seeds = Almanac.First().Split(": ")[1].Split(" ").Select(long.Parse).ToArray();
        var mappings = new List<string>();
        for (var i = 2; i < Almanac.Skip(2).Count() + 2; i++)
        {
            if (Almanac[i] == "")
            {
                seeds = Convert(seeds, mappings);
                mappings.Clear();
                continue;
            }

            mappings.Add(Almanac[i]);
        }

        seeds = Convert(seeds, mappings);
        return seeds.Min();
    }

    private static long[] Convert(long[] numbers, List<string> sourceCategory)
    {
        var converted = new List<long>();
        foreach (var number in numbers)
        {
            var destination = number;
            foreach (var category in sourceCategory.Skip(1))
            {
                var (destinationStart, sourceStart, range) = category.Split(" ") switch
                {
                    var cat => (long.Parse(cat[0]), long.Parse(cat[1]), long.Parse(cat[2]))
                };
                if (number < sourceStart || number >= sourceStart + range) continue;
                destination = Math.Abs(number - sourceStart) + destinationStart;
                break;
            }

            converted.Add(destination);
        }

        return converted.ToArray();
    }

    
    // explanation https://www.youtube.com/watch?v=NmxHw_bHhGM
    public static long Two()
    {
        var inputs = Almanac.First().Split(": ")[1].Split(" ").Select(long.Parse).ToArray();
        var seeds = new List<(long start, long end)>();
        for (var i = 0; i < inputs.Length - 1; i += 2)
        {
            seeds.Add((inputs[i], inputs[i] + inputs[i + 1]));
        }

        var mappings = new List<string>();
        for (var i = 2; i < Almanac.Skip(2).Count() + 2; i++)
        {
            if (Almanac[i] == "")
            {
                seeds = Convert(seeds, mappings);
                mappings.Clear();
                continue;
            }

            mappings.Add(Almanac[i]);
        }

        seeds = Convert(seeds, mappings);
        return seeds.Min().start;
    }
    
    private static List<(long start, long end)> Convert(List<(long start, long end)> numbers, List<string> sourceCategory)
    {
        var newNums = new List<(long start, long end)>();
        while (numbers.Count > 0)
        {
            var (start, end) = numbers.First();
            numbers.RemoveAt(0);

            foreach (var category in sourceCategory.Skip(1))
            {
                var (destinationStart, sourceStart, range) = category.Split(" ")
                    switch { var cat => (long.Parse(cat[0]), long.Parse(cat[1]), long.Parse(cat[2])) };
                var overlappingStart = Math.Max(start, sourceStart);
                var overlappingEnd = Math.Min(end, sourceStart + range);
                if (overlappingStart < overlappingEnd)
                {
                    newNums.Add((overlappingStart - sourceStart + destinationStart, overlappingEnd - sourceStart + destinationStart));
                    if (overlappingStart > start)
                        numbers.Add((start, overlappingStart));
                    if (end > overlappingEnd)
                        numbers.Add((overlappingEnd, end));
                    break;
                }

                if (category == sourceCategory.Last())
                    newNums.Add((start, end));
            }
        }
        return newNums;
    }
}