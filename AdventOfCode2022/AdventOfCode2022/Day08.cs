namespace AdventOfCode2022;

public static class Day08
{
    private static readonly List<string> Input = Util.ReadFile("/day08/input");
    public static (int VisbleTrees, int Highestscenic) Result = VisibleInteriors();

    private static (int, int ) VisibleInteriors()
    {
        var matrix = Input.ToMatrix();
        var visibleTreesFromPosition = new List<int>();
        var visble = 0;
        for (var i = 1; i < matrix.GetLength(0) - 1; i++)
        {
            for (var j = 1; j < matrix.GetLength(1) - 1; j++)
            {
                var scenic = 1;
                var column = matrix.GetColumn(j);
                var row = matrix.GetRow(i);
                if (matrix[i, j] > row.Take(j).Max() || matrix[i, j] > row.Skip(j + 1).Max() ||
                    matrix[i, j] > column.Take(i).Max() || matrix[i, j] > column.Skip(i + 1).Max())
                    visble++;
                scenic *= VisibleTrees(row.Take(j).Reverse(), matrix[i, j]);
                scenic *= VisibleTrees(row.Skip(j + 1), matrix[i, j]);
                scenic *= VisibleTrees(column.Take(i).Reverse(), matrix[i, j]);
                scenic *= VisibleTrees(column.Skip(i + 1), matrix[i, j]);
                visibleTreesFromPosition.Add(scenic);
            }
        }

        return (visble + (matrix.GetLength(0) - 1) * 4, visibleTreesFromPosition.Max());
    }

    private static int VisibleTrees(IEnumerable<int> row, int number)
    {
        var visble = row.TakeWhile(x => x < number).Count();
        if (visble == row.Count())
            return visble;
        return visble + 1;
    }
}