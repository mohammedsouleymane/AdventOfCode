namespace Aoc2023;

public class Day11
{
    private static readonly List<string> lines = Util.ReadFile("/day11/input");
    private static char[,] _matrix =  Util.ReadFile("/day11/input").ToCharMatrix();
    private static List<int> _mtC = new ();
    private static List<int> _mtR = new ();
    private static void FillEmptyRowsAndColumns()
    {
        for (var i = 0; i < _matrix.GetLength(0); i++)
        {
            if(_matrix.GetRow(i).Any(x=> x== '#')) continue;
            _mtR.Add(i);
        }
        for (var i = 0; i < _matrix.GetLength(1); i++)
        {
            if(_matrix.GetColumn(i).Any(x=> x== '#')) continue;
            _mtC.Add(i);
        }
    }
    private static long ManhattanDistance((int i, int j) start,(int i,int j) end , long scale)
    {
        long sum = 0;
        for (var i = Math.Min(start.i, end.i); i < Math.Max(start.i, end.i); i++) 
        {
            sum += _mtR.Contains(i) ? scale : 1;
        }
        for (var i = Math.Min(start.j, end.j); i < Math.Max(start.j, end.j); i++)
        {
            sum += _mtC.Contains(i) ? scale : 1;
        }
        return sum;
    }

    private static long CalculateDistance(int scale)
    { 
        FillEmptyRowsAndColumns();
        var coordinates = new List<(int, int)>();
        _matrix = new char[lines.Count, lines.First().Length];
        for (var i = 0; i < lines.Count; i++)
        {
            for (var j = 0; j < lines.First().Length; j++)
            {
                _matrix[i, j] = lines[i][j];
                if (_matrix[i, j] != '.') coordinates.Add((i, j));
            }
        }
        long sum = 0;
        for (var i = 0; i < coordinates.Count; i++)
        {
            for (var j = i+1; j < coordinates.Count; j++)
            {
                sum += ManhattanDistance(coordinates[i], coordinates[j], scale);
            }
        }
        return sum;
    }

    public static long  One => CalculateDistance(2);
    public static long Two => CalculateDistance(1000000);
}