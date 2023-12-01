using System.Runtime.InteropServices;
using Dijkstra;

namespace Aoc2022;

public static class Day12
{
    private static (int, int) _start = (-1, -1);
    private static (int, int) _end = (-1, -1);
    private static readonly List<(int, int)> PossibleStarts = new();
    private static readonly char[,] Grid = Matrix(Util.ReadFile("/day12/input").ToArray());
    public static readonly int ShortestFromPossibleStarts = GetShortest(1).Min();
    public static readonly int ShortestFromS = GetShortest(2).Min();

    
    private static IEnumerable<int> GetShortest(int part)
    {
        List<int> shortestForAllStarts = new();
        var starts = part == 1 ? new List<(int, int)>{_start} : PossibleStarts;
        foreach (var start in CollectionsMarshal.AsSpan(starts))
        {
            var dijkstra = new Dijkstra<char>(Grid, start, _end, NeighborsMaxPlusOne);
            var shortestPath = dijkstra.ShortestCost();
            if (shortestPath != 0)
                shortestForAllStarts.Add(shortestPath);
            
           
        }

        return shortestForAllStarts;
    }

    private static List<(int x, int y)> NeighborsMaxPlusOne((int i, int j) square)
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
        return neighbors;
    }
    private static char[,] Matrix(IReadOnlyCollection<string> list)
    {
        var matrix = new char[list.Count, list.First().Length];
        for (var i = 0; i < matrix.GetLength(0); i++)
        {
            for (var j = 0; j < matrix.GetLength(1); j++)
            {
                _start = list.ToArray()[i][j] == 'S' && _start.Item1 == -1 ? (i, j) : _start;
                _end = list.ToArray()[i][j] == 'E' && _end.Item1 == -1 ? (i, j) : _end;
                matrix[i, j] = list.ToArray()[i][j] == 'S' ? 'a' : list.ToArray()[i][j] == 'E' ? 'z' : list.ToArray()[i][j];
                if(matrix[i,j] == 'a')
                    PossibleStarts.Add((i,j));
            }
        }
        return matrix;
    }
}