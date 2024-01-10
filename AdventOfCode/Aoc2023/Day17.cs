namespace Aoc2023;

public class Day17
{
    private static readonly char[,] Grid = Util.ReadFile("/day17/test").ToCharMatrix();

    private static List<(int x, int y)> NeighborsMaxPlusOne((int i, int j) square)
    {
        List<(int x, int y)> neighbors = new();
        if (Grid.GetLength(1) - 1 > square.j) // right
            neighbors.Add((square.i, square.j + 1));
        if (0 < square.j) // left
            neighbors.Add((square.i, square.j - 1));
        if (0 < square.i) // up
            neighbors.Add((square.i - 1, square.j));
        if (Grid.GetLength(0) - 1 > square.i) // down
            neighbors.Add((square.i + 1, square.j));
        return neighbors;
    }

    public static int One()
    {
         var matrix = new int[Grid.GetLength(0),Grid.GetLength(1)];

         for (var i = 0; i < Grid.GetLength(0); i++)
         {
             for (var j = 0; j < Grid.GetLength(1); j++)
             {
                 matrix[i, j] = int.Parse(Grid[i, j].ToString());
             }
         }
        var dijkstra = new Dijkstra<int>(matrix, (0,0) ,(Grid.GetLength(0) - 1, Grid.GetLength(1) - 1), NeighborsMaxPlusOne);
        return dijkstra.ShortestCost();
    }
}