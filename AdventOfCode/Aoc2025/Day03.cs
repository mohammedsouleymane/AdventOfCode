namespace Aoc2025;

public static class Day03
{
    private static readonly List<string> Banks = Util.ReadFile("/day03/input");


    public static long TotalOutputJoltage(int n = 2)
    {
        long sum = 0;
        foreach (var bank in Banks)
        {
            var temp = bank;   
            var num = "";
            for (var j = 1; j <= n ; j++)
            {
                var largestBattery = temp[..^(n - j)].Max(); // we need n numbers so we have to take till we have n - j remaining numbers
                var maxIndex = Array.IndexOf(temp.ToCharArray(), largestBattery);
                temp = temp[(maxIndex+1)..]; // cuts off at max index ex 4532 if n = 2 we take 5 and keep 32
                num += largestBattery;
            }
            sum += long.Parse(num);
        }

        return sum;
    }
}