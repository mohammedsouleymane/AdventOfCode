namespace Aoc2024;

public static class Day18
{
    private static readonly (int, int)[] Input = Util.ReadFile("/day18/input")
        .Select(x => x.Split(",").Select(int.Parse).ToArray()).Select(x => (x[1], x[0])).ToArray();

    private static readonly (int i, int j) End = (70, 70);
     private static readonly IEnumerable<(int, int)> Fallen = Input.Take(1024);
    private static readonly char[,] Map = new char[End.i + 1, End.j + 1];
    private static List<(int i, int j)> Neighbors((int i,int j) current)
    {
        List<(int,int)> directions =  [(0, 1), (0, -1), (1, 0), (-1, 0)];
        
        return directions
            .Select(direction => current.Add(direction))
            .Where(position => Map.InBounds(position) && Map.GetELementAt(position) != '#')
            .ToList();
    }

    private static void FillMap(List<(int i ,int j)> positions) =>  positions.ForEach((position) => Map[position.i, position.j] = '#');
    
    
    public static int ShortestPath(bool one =true)
    {
        if(one) FillMap(Fallen.ToList());
        SortedSet<(int p,(int i, int j) pos)> pqueue = []; 
        HashSet<(int, int)> visited = []; 
        pqueue.Add((0, (0,0))); 
        while (pqueue.Count > 0 && pqueue.First().pos != End)
        {
            var current = pqueue.First();
            pqueue.Remove(current);
            Neighbors(current.pos)
                .Where(pos => !visited.Contains(pos)).ToList()
                .ForEach(pos =>pqueue.Add( ( current.p + 1, pos)));
            visited.Add(current.pos);
        }

        return pqueue.Count == 0 ? -1 : pqueue.First().p;
    }
        
    public static (int, int) CutOff()
    {
        var eliminated = 1;
        FillMap(Input.ToList());
        while (ShortestPath(false) == -1) // start eliminating from the back
        { 
            var (i,j) = Input[^++eliminated];
            Map[i, j] = '.';
        }

        var (y, x) = Input[^eliminated];
        return (x,y) ;
    }
}