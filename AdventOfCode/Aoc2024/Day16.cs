namespace Aoc2024;

public static class Day16
{
    private static readonly char[,] Map = Util.ReadFile("/day16/input").ToCharMatrix();
    
    private static List<((int i, int j), (int i, int j))> Neighbors((int i,int j) current, (int,int) dir)
    {
        List<(int,int)> directions =  [(0, 1), (0, -1), (1, 0), (-1, 0)];
        directions = directions.Where(x => Degree90(dir, x)).ToList();
        directions.Add(dir);
        return directions
            .Where(direction => Map.GetELementAt(current.Add(direction)) != '#')
            .Select(direction => (current.Add(direction), direction)).ToList();
    }

    private static bool Degree90((int i, int j) dir1, (int i, int j) dir2)
    {
        return Math.Abs(dir1.i) != Math.Abs(dir2.i) && Math.Abs(dir1.j) != Math.Abs(dir2.j);
    }
    
    public static readonly int ShortestCost = ShortestPath();
    //dijkstra algorithm
    private static int ShortestPath()
    { 
        SortedSet<(int p,(int i, int j) pos, (int y,int x) dir)> pqueue = []; 
        HashSet<(int, int)> visited = []; 
        pqueue.Add((0, Map.Find('S'), (0, 1))); 
        while (Map.GetELementAt(pqueue.First().pos) != 'E' )
        {
            var current = pqueue.First();
            pqueue.Remove(current);
            foreach (var (pos, dir) in Neighbors(current.pos, current.dir))
            {
                if(visited.Contains(pos)) continue;
                pqueue.Add( ((Degree90(current.dir, dir) ? 1001 : 1) + current.p, pos, dir));
            }
            visited.Add(current.pos);
        }
        return pqueue.First().p;
    } 
    private static readonly HashSet<(int,int)> Seats = []; 
    private static readonly Dictionary<((int, int), (int, int)), int> Visited = [];//positions and direction as key value the lowest cost
    //dfs  
    private static void AllPaths(HashSet<(int, int)> seen, (int, int) current, (int, int) dir, int cost, HashSet<(int, int)> path)
    {
         if(Visited.GetValueOrDefault((current, dir), int.MaxValue) < cost) return; //if positions and direction combinations have a lower cost than the current cost return
         Visited[(current, dir)] = cost;
         path.Add(current);
         if (cost == ShortestCost && Map.GetELementAt(current) == 'E')
             path.ToList().ForEach(x => Seats.Add(x));
         else if (cost < ShortestCost)
         {
             seen.Add(current);
             foreach (var (pos, ndir) in Neighbors(current, dir))
             {
                 if (seen.Contains(pos)) continue;
                 AllPaths(seen, pos, ndir, (Degree90(dir, ndir) ? 1001 : 1) + cost, path);
             }
             seen.Remove(current);
         }
         path.Remove(current); 
    }

    public static int GoodSeats()
    {
        AllPaths([], Map.Find('S'), (0,1),0, []);
        return Seats.Count;
    }
}