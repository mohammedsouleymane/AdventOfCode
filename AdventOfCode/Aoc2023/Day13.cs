namespace Aoc2023;

public class Day13
{
    private static int FindMirror(List<string> mirrors, int smugde = 0)
    {
        var s = mirrors.Count % 2;
        for (var i = s; i < mirrors.Count; i++)
        {
            var h = (mirrors.Count + i)/ 2;
            var left = mirrors.Skip(i).Take(h-i).ToStr("\n");
            var right = mirrors.Skip(h).Take(h-i).Reverse().ToStr("\n");
            if (left.Zip(right).Count(x => x.First != x.Second) != smugde)
            {
                i++;
                continue;
            };
            return h ;
        }
        mirrors.Reverse();
        for (var i = s; i < mirrors.Count; i++)
        {
            var h = (mirrors.Count + i)/ 2;
            var left = mirrors.Skip(i).Take(h-i).ToStr("\n");
            var right = mirrors.Skip(h).Take(h-i).Reverse().ToStr("\n");
            
            if (left.Zip(right).Count(x => x.First != x.Second) != smugde)
            {
                i++;
                continue;
            };
            return mirrors.Count - h;
        }
        
        return 0;
    }
    
    public static int One()
    {
        var sum = 0;
        foreach (var line in  Util.ReadFile("/day13/input").SplitToChucks(""))
        {
            var z = FindMirror(line);
            if (z != 0)
                sum += z * 100;
            else
                sum += FindMirror( Enumerable.Range(0, line.First().Length).Select(i => line.Select(x => x[i]).ToStr()).ToList());
        }
        return sum;
    }
    public static int Two()
    {
        var sum = 0;
        foreach (var line in  Util.ReadFile("/day13/input").SplitToChucks(""))
        {
            var z = FindMirror(line,1);
            if (z != 0)
                sum += z * 100;
            else
                sum += FindMirror( Enumerable.Range(0, line.First().Length).Select(i => line.Select(x => x[i]).ToStr()).ToList(),1);
        }
        return sum;
    }
}