namespace Aoc2023;

public static class Day10
{
    private static readonly char[,] Matrix = Util.ReadFile("/day10/input").ToCharMatrix();

    private static bool MoveUp(char c) => "S|JL".Contains(c);
    private static bool ReceiveUp(char c) => "F7|".Contains(c);
    private static bool MoveDown(char c) => "S|F7".Contains(c);
    private static bool ReceiveDown(char c) => "|JL".Contains(c);
    private static bool MoveLeft(char c) => "S-7J".Contains(c);
    private static bool ReceiveLeft(char c) => "-LF".Contains(c);
    private static bool MoveRight(char c) => "S-LF".Contains(c);
    private static bool ReceiveRight(char c) => "-J7".Contains(c);
    public static int One()
    {
        var start = (-1, -1);
        for (var i = 0; i < Matrix.GetLength(0); i++)
        {
            for (var j = 0; j < Matrix.GetLength(1); j++)
            {
                if (Matrix[i, j] != 'S') continue;
                start = (j, i);
                break;
            }
            if(start != (-1,-1)) break;
        }

        var seen = new List<(int, int)>{start};
        var queue = new Queue<(int x, int y)>();
        queue.Enqueue(start);
        while (queue.Any()) // flood fill with bfs
        {
            var (x,y) = queue.Dequeue();
            var curr = Matrix.GetELementAt((x,y));
            if (y > 0 && MoveUp(curr) && ReceiveUp(Matrix.GetELementAt((x, y - 1))) && !seen.Contains((x, y -1)))
            {
                seen.Add((x, y -1));
                queue.Enqueue((x, y  - 1));
            }
            if (y < Matrix.GetLength(1) && MoveDown(curr) && ReceiveDown(Matrix.GetELementAt((x, y + 1))) && !seen.Contains((x, y + 1)))
            {
                seen.Add((x, y + 1));
                queue.Enqueue((x, y  + 1));
            }
            
            if (x > 0 && MoveLeft(curr) && ReceiveLeft(Matrix.GetELementAt((x-1, y ))) && !seen.Contains((x - 1, y)))
            {
                seen.Add((x - 1, y));
                queue.Enqueue((x - 1, y));
            }
            if (x < Matrix.GetLength(0) && MoveRight(curr) && ReceiveRight(Matrix.GetELementAt((x+1, y ))) && !seen.Contains((x + 1, y)))
            {
                seen.Add((x + 1, y));
                queue.Enqueue((x + 1, y));
            }
        }

        return seen.Count / 2;
    }
}