namespace Aoc2024;

public static class Day12
{
    private static readonly char[,] Grid = Util.ReadFile("/day12/input").ToCharMatrix();
    
    private static int Region(int x, int y, HashSet<(int,int)> seen, bool two = false)
    {
        var current = Grid[x, y];
        var perimeter = 0;
        var queue = new Queue<(int, int)>();
        queue.Enqueue((x,y));
        var count = 0;
        HashSet<(double,double)> pos = new();
        while (queue.Count != 0)
        {
            var (i,j) = queue.Dequeue();
            if (!Grid.InBounds((i, j)) || Grid[i, j] != current)
            {
                perimeter++;
                continue;
            }
            
            if(!seen.Add((i, j))) continue;
            if(two) pos.Add((i, j));
            count++;
            queue.Enqueue((i+1,j)); 
            queue.Enqueue((i-1,j));
            queue.Enqueue((i,j+1));
            queue.Enqueue((i,j - 1));
        }
        if (!two) return perimeter * count;
        HashSet<(double, double)> possibleCorners = [];
        List<(double i, double j)> direction = [(-0.5, -0.5), (0.5, -0.5), (-0.5, 0.5), (0.5, 0.5)];
        foreach (var (i,j) in pos)
        {
            foreach (var (ni, nj ) in direction.Select(p=> (i + p.i, j+p.j)))
            {
                possibleCorners.Add((ni,nj));
            }
        }

        var sides = 0;
        foreach (var (ci,cj) in possibleCorners)
        { 
            var connectedCoo = direction.Select(p => (ci + p.i, cj + p.j)).Where(p => pos.Contains(p)).ToArray();
            switch (connectedCoo.Length) 
            { 
                case 1 or 3:
                    sides += 1;
                    break; 
                case 2: 
                {
                    if (connectedCoo.First().Subtract(connectedCoo.Last()) is (1,1)  or (-1,-1) or (-1,1) or (1,-1))
                        sides += 2;
                    break;
                } 
            }
        }
        return sides * count;
    }

    private static (int, int) Next(HashSet<(int, int)> seen)
    {
        for (var i = 0; i < Grid.GetLength(0); i++)
        {
            for (var j = 0; j < Grid.GetLength(1); j++)
            {
                if (!seen.Contains((i, j)))
                    return (i, j);
            }
        }
        return (-1, -1);
    }
    public static int PriceOfAllRegion(bool two=false)
    {
        var seen = new HashSet<(int, int)>();
        var sum = 0;
        var (x,y) = (0, 0);
        while (x != -1)
        {
            sum +=  Region(x, y, seen, two);
            (x, y) = Next(seen);
        }
        return sum;
    }
}