namespace Aoc2024;

public static class Day20
{
    private static readonly char[,] Map = Util.ReadFile("/day20/input").ToCharMatrix();
    private static readonly (int, int) End = Map.Find('E');
    private static readonly (int, int) Start = Map.Find('S');

    private static List<((int i, int j),(int i ,int j))> Neighbors((int i,int j) current)
    {
        List<(int,int)> directions =  [(0, 1), (0, -1), (1, 0), (-1, 0)];
        
        return directions
            .Select(direction => (current.Add(direction), direction))
            .Where(position => Map.InBounds(position.Item1))
            .ToList();
    }

    private static int ManhattanDistance((int i, int j) start)
    {
        var (i, j) = End.Subtract(start);
        return Math.Abs(i) + Math.Abs(j);
    }

    private static readonly Dictionary<((int i, int j), (int x, int y)), int> Cache = new();
    private static int ShortestPath(int cost, int max)
    {
        SortedSet<(int p,(int i, int j) pos)> pqueue = []; 
        HashSet<(int, int)> visited = []; 
        pqueue.Add((cost, Start)); 
      
        while ( pqueue.First().p <= max && pqueue.First().pos != End)
        {
            var current = pqueue.First();
            pqueue.Remove(current);

            foreach (var (pos,dir) in Neighbors(current.pos))
            {
                if (Cache.ContainsKey((pos, dir)) && Cache[(pos,dir)] < current.p + 1) continue; 
                if(Map.GetELementAt(pos) != '#' && !visited.Contains(pos)) continue;
                pqueue.Add((current.p + 1, pos));
            }
            visited.Add(current.pos);
        }
        return pqueue.First().p;
    }
    
    public static int ShortestPath(bool one = true)
    {
        SortedSet<(int p,(int i, int j) pos)> pqueue = []; 
        HashSet<(int, int)> visited = [];
        HashSet< (int, (int, int))> barriers = [];
        pqueue.Add((0, Start)); 
        while (pqueue.Count > 0 && pqueue.First().pos != End)
        {
            var current = pqueue.First();
            pqueue.Remove(current);

            foreach (var (pos, dir) in Neighbors(current.pos))
            {
                if (visited.Contains(pos)) continue;
                if (Map.GetELementAt(pos) == '#')
                {
                    barriers.Add((current.p + 1, pos));
                    continue;
                }

                Cache[(pos, dir)] = current.p + 1; 
                pqueue.Add((current.p + 1, pos));
            }
            
            visited.Add(current.pos);
        }
        var shortest = pqueue.First().p;
        var sum = 0;
        foreach (var (p, (i,j)) in barriers.Order())
        {
            if (ManhattanDistance((i,j)) + p <= shortest- 100 && shortest - ShortestPath(p, shortest - 100) >= 100)
                sum++;
        }

        return sum;
    }
}