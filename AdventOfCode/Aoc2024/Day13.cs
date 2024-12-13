namespace Aoc2024;

public static class Day13
{
    private static readonly List<string[]> Machines = Util.ReadFile("/day13/input").Chunk(4).ToList();
    //using the substitution method
    private static long SolveEquation(List<long[]> numbers, long[] results, long max)
    {
        var rx = results[0];
        var ry = results[1];
        var x1 = numbers.First()[0];
        var x2 = numbers.Last()[0];
        var y1 = numbers.First()[1];
        var y2 = numbers.Last()[1];
        var b = (y1 * rx - x1 * ry) / (y1 * x2 - x1 * y2);
        var a = (rx - x2 * b) / x1;
        if((y1 * rx - x1 * ry) % (y1 * x2 - x1 * y2) == 0 &&  (rx - x2 * b) % x1 == 0 && a <= max && b <= max)
            return a * 3 + b ;
        return 0;
    }
    public static long RunMachines(long  add ,bool two = false)
    {
        var max = two ? add : 100;
        long sum = 0;
        foreach (var machine in Machines)
        {
            List<long[]> numbers = [];
            foreach (var slot in machine.Take(2))
            {
                var variables = slot.Split(": ")[1].Split(", ").Select(v=> long.Parse(v.Split("+")[1])).ToArray();
                numbers.Add(variables);
            }
            var results = machine[2].Split(": ")[1].Split(", ").Select(n => add + long.Parse(n.Split("=")[1])).ToArray();
            sum += SolveEquation(numbers, results, max);
        }

        return sum;
    }
}
