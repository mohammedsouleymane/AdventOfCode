namespace Aoc2015;

public class Day10
{

    
    public static long Expand()
    {
        var input = "3113322113";
        for (var i = 0; i < 40; i++)
        {
            input = input.Select((x, i) => i > 0 && x != input[i - 1] ? "|" + x : x.ToString()).ToStr();
            input = input.Split("|").Select(s =>  s.Length + s[0].ToString() ).ToStr();
        }

        return input.Length;

    }
}