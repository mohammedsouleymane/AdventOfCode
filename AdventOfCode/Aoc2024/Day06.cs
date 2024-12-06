namespace Aoc2024;

public static class Day06
{
    private static readonly char[,] Map = Util.ReadFile("/day06/test").ToCharMatrix();
    private static readonly (int,int)[] Moves = [(-1, 0), (0, 1), (1, 0), (0, -1)];
    private static (int , int ) FindStart()
    {
        for (var i = 0; i < Map.GetLength(0); i++)
        {
            for (var j = 0; j < Map.GetLength(1); j++)
            {
                if (Map[i, j] != '^') continue;
                return (i, j);
            }
        }
        return (-1, -1);
    }

    
    private static HashSet<(int, int, int)> BruteForce()
    {
        var copy = new HashSet<(int, int, int)>();
        var position = FindStart();
        var guard = 0;
        var (x, y) = Moves[guard];
        while (Map.InBounds((position.Item1 + x, position.Item2 + y)))
        {
            position = (position.Item1 + x, position.Item2 + y);
            if (Map.GetELementAt(position) == '#')
            {
                position = (position.Item1 - x , position.Item2 - y );
                guard = ++guard % 4;
            }
            else
            {
                if ( !copy.Add((position.Item1, position.Item2, guard))) return [];
            }
                
            
            (x, y) = Moves[guard];
        }
        return copy;
    }
    
    public static int Calculate(bool two = false)
    {
       
        if (!two) return BruteForce().Select(p => (p.Item1, p.Item2)).Distinct().Count();
        var newObstructions = new HashSet<(int, int)>();
        for (var i = 0; i < Map.GetLength(0); i++)
        {
            for (var j = 0; j < Map.GetLength(1); j++)
            {
                if(Map[i , j ] != '.') continue;
                    Map[i,j] = '#';
                    if (BruteForce().Count == 0)
                        newObstructions.Add((i,j));
                    Map[i,j] = '.';
            }
        }
        return newObstructions.Count;
    }
}