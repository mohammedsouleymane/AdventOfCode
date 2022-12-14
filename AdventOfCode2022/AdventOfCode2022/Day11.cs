namespace AdventOfCode2022;

public static class Day11
{
    private static readonly List<string> Input = Util.ReadFile("/day11/input").Select(x => x.Trim()).ToList();
    private static readonly Dictionary<long, long> Inspected = new();

    private static readonly long Mod = Input.Where(x => x.StartsWith("Test")).Select(x => int.Parse(x.Split(" ")[3]))
        .Aggregate((i, i1) => i * i1);

    public static long LevelOfMonkeyBusiness(int rounds = 20)
    {
        Inspected.Clear();
        RunRounds(rounds);
        return Inspected.Values.Max() * Inspected.Values.OrderByDescending(x => x).Take(2).Last();
    }

    private static void RunRounds(int rounds)
    {
        var items = GetMonkeyItems();
        for (var j = 0; j < rounds; j++)
        {
            foreach (var monkey in items)
            {
                while (monkey.Value.Count > 0)
                {
                    var worryLevel = rounds == 20 ? GetWorryLevel(monkey.Key, monkey.Value.Dequeue()) / 3: GetWorryLevel(monkey.Key, monkey.Value.Dequeue()) % Mod;
                    items[RunTest(monkey.Key, worryLevel)].Enqueue(worryLevel);
                }
            }
        }
    }

    private static Dictionary<long, Queue<long>> GetMonkeyItems()
    {
        Dictionary<long, Queue<long>> items = new();
        foreach (var line in Input.Where(x => x.StartsWith("Monkey")))
        {
            var monkeyItems = Input.SkipWhile(x => x != line).Skip(1).First()[16..].Split(",").ToList();
            items.Add(int.Parse(line[..^1].Split(" ")[1]), new Queue<long>(monkeyItems.Select(long.Parse)));
            Inspected.Add(int.Parse(line[..^1].Split(" ")[1]), 0);
        }
        return items;
    }

    private static long GetWorryLevel(long monkey, long old)
    {
        var operation = Input.SkipWhile(x => x != $"Monkey {monkey}:").Take(3).Last();
        return RunOperation(old, operation);
    }

    private static long RunTest(long monkey, long worryLevel)
    {
        ++Inspected[monkey];
        var test = Input.SkipWhile(x => x != $"Monkey {monkey}:").Skip(3).Take(3).ToArray();
        return int.Parse(worryLevel % int.Parse(test[0].Split(" ")[3]) == 0
            ? test[1].Split(" ")[5]
            : test[2].Split(" ")[5]);
    }

    private static long RunOperation(long old, string operation)
    {
        long number;
        try { number = int.Parse(operation.Split(" ")[5]); }
        catch (Exception) { number = old; }
        return operation.Contains('+') ? old + number : old * number;
    }
}