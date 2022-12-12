using System.Runtime.InteropServices;

namespace AdventOfCode2022;

public static class Day12
{
    private static (int, int) _start = (-1, -1);
    private static (int, int) _end = (-1, -1);
    private static readonly List<(int, int)> PossibleStarts = new();
    private static readonly char[,] Grid = Matrix(Util.ReadFile("/day12/input"));
    public static readonly int ShortestFromPossibleStarts = GetShortest(1).Min();
    public static readonly int ShortestFromS = GetShortest(2).Min();

    
    private static IEnumerable<int> GetShortest(int part)
    { 
        SortedSet<(int p, int x, int y)> costs = new();
        List<(int, int)> visited = new();
        List<int> shortestForAllStarts = new();
       var starts = part == 1 ? new List<(int, int)>{_start} : PossibleStarts;
        foreach (var start in CollectionsMarshal.AsSpan(starts))
        {
            var square = (start.Item1, start.Item2);
            NeighborsMaxPlusOne(start, visited).ForEach(x => costs.Add((1, x.x, x.y)));
            visited.Add(square);
            while (square != _end)
            {
                if (!costs.Any())
                    break;
                square = (costs.First().x, costs.First().y);
                visited.Add(square);
                foreach (var sq in CollectionsMarshal.AsSpan(NeighborsMaxPlusOne(square, visited)))
                {
                    costs.Add((costs.First().p + 1, sq.x, sq.y));
                }
                if(square != _end)
                    costs.Remove(costs.First());
            }
            if(costs.Any())
                shortestForAllStarts.Add(costs.First().p);
            costs.Clear();
            visited.Clear();
        }

        return shortestForAllStarts;
    }

    private static List<(int x, int y)> NeighborsMaxPlusOne((int i, int j) square, List<(int, int)> visited )
    {
        List<(int x, int y)> neighbors = new();
        if (Grid.GetLength(1) - 1 > square.j && Grid[square.i, square.j] + 1 >= Grid[square.i, square.j + 1]) // right
            neighbors.Add((square.i, square.j + 1));
        if (0 < square.j && Grid[square.i, square.j] + 1 >= Grid[square.i, square.j - 1]) // left
            neighbors.Add((square.i, square.j - 1));
        if (0 < square.i && Grid[square.i, square.j] + 1 >= Grid[square.i - 1, square.j]) // up
            neighbors.Add((square.i - 1, square.j));
        if (Grid.GetLength(0) - 1 > square.i && Grid[square.i, square.j] + 1 >= Grid[square.i + 1, square.j]) // down
            neighbors.Add((square.i + 1, square.j));
        return neighbors.Where(x=> !visited.Contains(x)).ToList();
    }
    private static char[,] Matrix(IEnumerable<string> list)
    {
        var enumerable = list as string[] ?? list.ToArray();
        var matrix = new char[enumerable.Count(), enumerable.First().Length];
        for (var i = 0; i < matrix.GetLength(0); i++)
        {
            for (var j = 0; j < matrix.GetLength(1); j++)
            {
                _start = enumerable.ToArray()[i][j] == 'S' && _start.Item1 == -1 ? (i, j) : _start;
                _end = enumerable.ToArray()[i][j] == 'E' && _end.Item1 == -1 ? (i, j) : _end;
                matrix[i, j] = enumerable.ToArray()[i][j] == 'S' ? 'a' : enumerable.ToArray()[i][j] == 'E' ? 'z' : enumerable.ToArray()[i][j];
                if(matrix[i,j] == 'a')
                    PossibleStarts.Add((i,j));
            }
        }
        return matrix;
    }
}