namespace Aoc2015;

public class Day09
{
    public static (int, int) Solve()
    {
        var edges = Util.ReadFile("/day09/input");
        var dict = new Dictionary<string, int>();
        var i = 0;
        foreach (var edge in edges)
        {
            var e = edge.Split(" to ");
            var from = e[0];
            var to = e[1].Split(" = ")[0];
            if(!dict.ContainsKey(from))
                dict.Add(from, i++);
            if(!dict.ContainsKey(to))
                dict.Add(to, i++);
        }

        var matrix = new int [dict.Count, dict.Count];

        foreach (var edge in edges)
        {
            var e = edge.Split(" to ");
            var from = e[0];
            var to = e[1].Split(" = ")[0];
            var weight = int.Parse(e[1].Split(" = ")[1]);
            matrix[dict[from], dict[to]] = weight;
            matrix[dict[to], dict[from]] = weight;
        }

        for (int j = 0; j< matrix.GetLength(0); j++)
        {
            var li = Enumerable.Range(0, matrix.GetLength(0) ).ToList();
            li.Remove(j);
            Calc(matrix, 0, j, li );
        }
        
        return (Costs.Min(), Costs.Max());
    }

    private static readonly List<int> Costs = [];
    private static void Calc(int[,] matrix, int cost, int x, List<int> list)
    {
        
        if (list.Count == 0)
            Costs.Add(cost);
        else
        {
            foreach (var i in list)
            {
                var li = list.ToList();
                li.Remove(i);
                Calc(matrix, cost + matrix[x,i], i, li);
            }
        }
        
    }
}