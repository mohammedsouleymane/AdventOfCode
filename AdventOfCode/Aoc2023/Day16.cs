namespace Aoc2023;

public static class Day16
{
    private static readonly char[,] Matrix = Util.ReadFile("/day16/input").ToCharMatrix();
    private static readonly (int i, int j) Up = (-1, 0);
    private static readonly (int i, int j) Down = (1, 0);
    private static readonly (int i, int j) Left = (0, -1);
    private static readonly (int i, int j) Right = (0, 1);

    private static int EnergizeTiles(int pi, int pj, int di, int dj)
    {
        
        var seen = new List<(int, int, int, int)>();
        var queue = new Queue<(int pi, int pj, int di, int dj)>();
        queue.Enqueue((pi, pj, di, dj));
        while (queue.Count > 0)
        {
            var c = queue.Dequeue();
            (int i, int j) result = (c.pi + c.di, c.pj + c.dj);
            if (result.i >= Matrix.GetLength(0) || result.j >= Matrix.GetLength(1) || result.i < 0 ||
                result.j < 0) continue;

            var character = Matrix.GetELementAt(result);
            if (character == '.' || (character == '-' && ((c.di, c.dj) == Left || (c.di, c.dj) == Right)) ||
                (character == '|' && ((c.di, c.dj) == Up || (c.di, c.dj) == Down)))
            {
                if (!seen.Contains((result.i, result.j, c.di, c.dj)))
                {
                    seen.Add((result.i, result.j, c.di, c.dj));
                    queue.Enqueue((result.i, result.j, c.di, c.dj));
                }
                continue;
            }

            switch (character)
            {
                case '|':
                    foreach (var co in new List<(int i, int j)>{Up, Down}.Where(co => !seen.Contains((result.i, result.j, co.i, co.j))))
                    {
                        seen.Add((result.i, result.j, co.i, co.j));
                        queue.Enqueue((result.i, result.j, co.i, co.j));
                    }
                    break;
                case '-':
                    foreach (var co in new List<(int i, int j)>{Left, Right}.Where(co => !seen.Contains((result.i, result.j, co.i, co.j))))
                    {
                        seen.Add((result.i, result.j, co.i, co.j));
                        queue.Enqueue((result.i, result.j, co.i, co.j));
                    }
                    break;
                case '/':
                    if (!seen.Contains((result.i, result.j, -c.dj, -c.di)))
                    {
                        seen.Add((result.i, result.j, -c.dj, -c.di));
                        queue.Enqueue((result.i, result.j, -c.dj, -c.di));
                    }
                    break;
                case '\\':
                    if (!seen.Contains((result.i, result.j, c.dj, c.di)))
                    {
                        seen.Add((result.i, result.j, c.dj, c.di));
                        queue.Enqueue((result.i, result.j, c.dj, c.di));
                    }
                    break;
            }
        }
        return seen.Select(x => (x.Item1, x.Item2)).Distinct().Count();
    }
    public static int One => EnergizeTiles(0, -1, 0, 1);
    

}