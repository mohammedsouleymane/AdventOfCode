namespace AdventOfCode2022;

public static class Day11
{
    private static List<string> input = Util.ReadFile("/day11/input").Select(x => x.Trim()).ToList();
    private static Dictionary<int, int> Inspected = new Dictionary<int, int>();

    public static int LevelOfMonkeyBusiness()
    {
        
        RunRounds();
        return Inspected.Values.Max() * Inspected.Values.OrderByDescending(x => x).Take(2).Last();
    }
    private static void RunRounds()
    {
        var items = GetMonkeyItems();
        for (var i = 0; i < 20; i++)
        {
            foreach (var monkey in items)
            {
                while (monkey.Value.Count > 0)
                {
                    var old = monkey.Value.Dequeue();
                    var worryLevel = GetWorryLevel(monkey.Key, old) / 3;
                    items[RunTest(monkey.Key, worryLevel)].Enqueue(worryLevel);
                }
           
            }
        }
        
    }
    private static Dictionary<int, Queue<int>> GetMonkeyItems()
    {
        Dictionary<int, Queue<int>> items = new();
        foreach (var line in input.Where(x=> x.StartsWith("Monkey")))
        {
            var monkeyItems = input.SkipWhile(x => x != line).Skip(1).First()[16..].Split(",").ToList();
            items.Add(int.Parse(line[..^1].Split(" ")[1]), new Queue<int>(monkeyItems.Select(int.Parse))) ;
            Inspected.Add(int.Parse(line[..^1].Split(" ")[1]), 0);
        }
        return items;
    }

    private static int GetWorryLevel(int monkey, int old)
    {
        var operation = input.SkipWhile(x => x != $"Monkey {monkey}:").Take(3).Last();
        return RunOperation(old, operation);
    }

    private static int RunTest(int monkey, int worryLevel)
    {
        ++Inspected[monkey];
        var test = input.SkipWhile(x => x != $"Monkey {monkey}:").Skip(3).Take(3).ToArray();
        if (worryLevel % int.Parse(test[0].Split(" ")[3]) == 0)
            return int.Parse(test[1].Split(" ")[5]);
        return int.Parse(test[2].Split(" ")[5]);

    }
    private static int RunOperation(int old, string operation )
    {
        int number;
        try{ number =  int.Parse(operation.Split(" ")[5]); }
        catch (Exception e) { number = old; }
 
        if (operation.Contains('+'))
            return old + number;
        return old * number;
    }
}