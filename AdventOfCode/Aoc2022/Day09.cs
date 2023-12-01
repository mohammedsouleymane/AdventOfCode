namespace Aoc2022;

public static class Day09
{
    private static readonly List<string> Moves = Util.ReadFile("/day09/sample");
    public static readonly int WhereTailsHasBeen = Move().Count();


    private static IEnumerable<(int, int)> Move((int, int) head = default)
    {
        var coordinates = new List<(int, int)>();
        var tails = new int[9];
        var j = 0;
        foreach (var move in Moves)
        {
            for (var i = 0; i < int.Parse(move.Split(" ")[1]); i++)
            {
                var old = coordinates.Count == 0 && j < 10 ? (0, 0) : coordinates.Last();
                (head, var tail) = Update(head, move);
                if (IsNeighbor(head, old)) continue;
                if (head.Item1 != old.Item1 && head.Item2 != old.Item2)
                {
                    var x = head.Item1 - old.Item1;
                    var y = head.Item2 - old.Item2;
                    tail = (old.Item1 + (x / Math.Abs(x)), old.Item2 + (y / Math.Abs(y)));
                    coordinates.Add(tail);
                }
                else
                    coordinates.Add(tail);
                j++;
            }
        }
        return coordinates.Distinct();
    }

    private static void Move2()
    {
        
    }
    private static ((int, int), (int, int)) Update((int x, int y) head, string move)
    {
        return move[0] switch
        {
            'L' => ((head.x, head.y - 1), head),
            'R' => ((head.x, head.y + 1), head),
            'U' => ((head.x + 1, head.y), head),
            _ => ((head.x - 1, head.y), head)
        };
    }
    
    private static bool IsNeighbor((int x, int y) head, (int x, int y) tail)
    {
        if (tail is {x: 0, y: 0}) return false; // skip start
        if (tail.x == head.x && tail.y == head.y) return true; // same position
        if (head.x - 1 == tail.x && tail.y == head.y || head.x + 1 == tail.x && tail.y == head.y)
            return true; // left or right
        if (head.x == tail.x && tail.y == head.y - 1 || head.x == tail.x && tail.y == head.y + 1)
            return true; // up or down
        if (head.x + 1 == tail.x && head.y + 1 == tail.y || head.x - 1 == tail.x && head.y - 1 == tail.y)
            return true; // dia: left up or right down
        return head.x + 1 == tail.x && head.y - 1 == tail.y ||
               head.x - 1 == tail.x && head.y + 1 == tail.y; // dia: left down or right up
    }
}