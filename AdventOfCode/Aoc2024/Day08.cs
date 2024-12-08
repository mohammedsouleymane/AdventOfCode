namespace Aoc2024;

public static class Day08
{
    private static readonly char[,] Map = Util.ReadFile("/day08/test").ToCharMatrix();
    private static Dictionary<char, List<(int i, int j)>> GroupByFreq()
    {
        var dict = new Dictionary<char, List<(int, int)>>();
        for (var i = 0; i < Map.GetLength(0); i++)
        {
            for (var j = 0; j < Map.GetLength(1); j++)
            {
                var c = Map[i, j];
               if(c == '.') continue;
               if (dict.TryGetValue(c, out var value))
                    value.Add((i, j));
               else
                   dict[c] = [(i, j)];
            }
        }
        return dict;
    }

    public static int AntiNodes(bool two = false)
    {
        var dict = GroupByFreq();
        var antiNodes = new HashSet<(int, int)>();

        foreach (var v in dict.Select(k => k.Value.ToArray()))
        {
            for (var i = 0; i < v.Length; i++)
            {
                for (var j = i + 1; j < v.Length; j++)
                {
                    var first = v[i];
                    var second = v[j];
                    var (di, dj) = (second.i - first.i, second.j - first.j);
                    
                    var k = 1;
                    while (Map.InBounds((first.i - di * k, first.j - dj * k)))
                    {
                        antiNodes.Add((first.i - di * k, first.j - dj * k));
                        if (!two) break;
                        k++;
                    }
                    k = 1;
                    while (Map.InBounds((second.i + di * k, second.j + dj * k)))
                    {
                        antiNodes.Add((second.i + di * k, second.j + dj * k));
                        if (!two) break;
                        k++;
                    }
                    if (!two) continue;
                    antiNodes.Add(first);
                    antiNodes.Add(second);
                }
            }
        }
        
        return antiNodes.Count;
    }
}