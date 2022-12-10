namespace AdventOfCode2022;

public static class Day10
{
    private static readonly List<string> Instructions = Util.ReadFile("/day10/input");
    public static readonly int SumOfSixSignalStrengths = SignalStrengths().Sum();

    private static Dictionary<int, int> Update(Dictionary<int, int> signalStrength , int cycle, int register)
    {
        if (!signalStrength.ContainsKey(cycle) && cycle is 20 or 60 or 100 or 140 or 180 or 220)
            signalStrength.Add(cycle, cycle * register );
        return signalStrength;
    }

    private static IEnumerable<int> SignalStrengths(int cycles = 0 ,int x = 1)
    {
        var signalStrengths = new Dictionary<int, int>();
        Console.Write("Part 2");
        foreach (var instruction in Instructions)
        {
            if (instruction == "noop")
            {
                DrawPixel(cycles % 40, x);
                cycles++;
            }
            else
            {
                for (var i = 0; i < 2; i++)
                {
                    DrawPixel(cycles % 40, x);
                    if (i == 1 && cycles is 20 or 60 or 100 or 140 or 180 or 220 )
                        signalStrengths.Add(cycles,x * cycles);
                    cycles++;
                }

                signalStrengths = Update(signalStrengths, cycles, x);
                x += int.Parse(instruction.Split(" ")[1]);
            }
            signalStrengths = Update(signalStrengths, cycles, x);
        }
        Console.WriteLine("\nPart 1");
        return signalStrengths.Values.ToList();
    }

    private static void DrawPixel(int cycles, int x)
    {
        var sufPrefix = cycles % 40 == 0  ?"\n" : " ";
        if(cycles + 1 %40 >= x && cycles + 1 < x%40 + 3)
            Console.Write($"{sufPrefix}#");
        else
            Console.Write($".{sufPrefix}");
    }
   
}