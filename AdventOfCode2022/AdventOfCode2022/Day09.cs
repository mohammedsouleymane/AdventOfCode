namespace AdventOfCode2022;

public static class Day09
{
    private static readonly List<string> Moves = Util.ReadFile("/day09/input");
    public static readonly int WhereTailsHasBeen = Move().Count();


    private static IEnumerable<(int, int)> Move((int, int) head = default)
    {
        List<(int, int)> coordinates = new();
        foreach (var move in Moves)
        {
            for (var i = 0; i < int.Parse(move.Split(" ")[1]); i++)
            {
                var old = coordinates.Count == 0 ? (0, 0) : coordinates.Last();
                (head, var tail) = Update(head, move);
                if (IsNeighbor(head, old)) continue;
                coordinates.Add(tail);
            }
        }
        return coordinates.Distinct();
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