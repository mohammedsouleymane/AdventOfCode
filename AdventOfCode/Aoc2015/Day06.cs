namespace Aoc2015;

public class Day06
{
    private static readonly List<string> Instructions = Util.ReadFile("/day06/input");

    public static int HowManyAreLit()
    {
        var lights = new bool[1000, 1000];
        var num = 0;
        foreach (var instruction in Instructions)
        {
            var split = instruction.Split(" through ");
            var end = split[1].Split(",").Select(int.Parse).ToArray();
            var start = split[0].Split(" ").Last().Split(",").Select(int.Parse).ToArray();
            for (var i = start[0]; i <= end[0]  ; i++)
            {
                for (var j = start[1]; j <= end[1]; j++)
                {
                    if (instruction.StartsWith("turn on"))
                        lights[i, j] = true;
                    else if (instruction.StartsWith("toggle"))
                        lights[i, j] = !lights[i, j];
                    else
                        lights[i, j] = false;
                }
            }
        }
        for (var i = 0; i <1000 ; i++)
        {
            for (var j = 0; j <1000; j++)
            {
                if (lights[i, j])
                    num++;
            }
        }
        return num;
    }
    
    public static long Brightness()
    {
        var lights = new int[1000, 1000];
        long num = 0;
        foreach (var instruction in Instructions)
        {
            var split = instruction.Split(" through ");
            var end = split[1].Split(",").Select(int.Parse).ToArray();
            var start = split[0].Split(" ").Last().Split(",").Select(int.Parse).ToArray();
            for (var i = start[0]; i <= end[0]  ; i++)
            {
                for (var j = start[1]; j <= end[1]; j++)
                {
                    if (instruction.StartsWith("turn on"))
                        lights[i, j]++;
                    else if (instruction.StartsWith("toggle"))
                        lights[i, j] += 2;
                    else if(lights[i, j] > 0)
                        lights[i, j]--;
                }
            }
        }
        for (var i = 0; i < 1000 ; i++)
        {
            for (var j = 0; j < 1000; j++)
            {
                    num += lights[i, j];
            }
        }
        return num;
    }
}