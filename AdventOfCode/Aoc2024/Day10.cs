namespace Aoc2024;

public static class  Day10
{
    private static readonly char[,] Map = Util.ReadFile("/day10/input").ToCharMatrix();


    private static Queue<(int i, int j, int x, int y)> StartingPositions()
    {
        var queue = new Queue<(int, int, int, int)>();
        for (var i = 0; i < Map.GetLength(0); i++)
        {
            for (var j = 0; j < Map.GetLength(1); j++)
            {
                if(Map[i,j] == '0')
                    queue.Enqueue((i,j,i,j));
            }
        }
        return queue;
    }

    private static List<(int i, int j)> Neighbor(int i, int j)
    {
        var neighbors = new List<(int, int)>();
        if (Map.InBounds((i + 1, j)))
            neighbors.Add((i+1,j));
        if (Map.InBounds((i - 1, j)))
            neighbors.Add((i-1,j));
        if (Map.InBounds((i , j +1)))
            neighbors.Add((i,j+1));
        if (Map.InBounds((i, j-1)))
            neighbors.Add((i,j - 1));
        return neighbors;
    }
    
    public static int TrailHeads()
    {
        var queue = StartingPositions();
        var startEnd = new HashSet<(int, int,int,int)>();// keeping starting and position to make sure the trail its a different trail
        
        while (queue.Count > 0)
        {
            var (i, j,x,y) = queue.Dequeue(); // x and y are your starting positions 
            var current = int.Parse(Map[i, j].ToString());
            foreach (var (i1,j1) in Neighbor(i,j))
            {
                if(Map[i1, j1] == '.') continue;
                var next =  int.Parse(Map[i1, j1].ToString());
                if (current + 1 == next && next == 9)
                    startEnd.Add((i1, j1,x,y));
                else if (current + 1 == next)
                    queue.Enqueue((i1, j1, x, y));
            }
        }
        return startEnd.Count;
    }
    
    public static int AltTrailHeads()
    {
        var queue = StartingPositions();
        var sum = 0;
        while (queue.Count > 0)
        {
            var (i, j,x,y) = queue.Dequeue(); //ignore x and y 
            var current = int.Parse(Map[i, j].ToString());
            foreach (var (i1,j1) in Neighbor(i,j))
            {
                if(Map[i1, j1] == '.') continue;
                var next =  int.Parse(Map[i1, j1].ToString());
                if (current + 1 == next && next == 9)
                    sum++;
                else if (current + 1 == next)
                    queue.Enqueue((i1, j1, x, y));
            }
        }
        return sum;
    }
}