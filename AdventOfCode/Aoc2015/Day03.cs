namespace Aoc2015;

public class Day03
{
    private static readonly string Directions = Util.ReadFile("/day03/input").First();

    private static (int, int) UpdatePosition((int x, int y) current, char direction)
    {
        return direction switch
        {
            '>' => (current.x, current.y + 1),
            '<' => (current.x, current.y - 1),
            '^' => (current.x + 1, current.y),
            'v' => (current.x - 1, current.y),
            _ => current
        };
    }
    public static int SoloVisited()
    {
        HashSet<(int, int)> visited = [(0, 0)];
        (int x , int y) current = (0, 0);
        foreach (var direction in Directions)
        {
            current = UpdatePosition(current,direction);
            visited.Add(current);
        }
        return visited.Count;
    }

    public static int RoboVisited()
    {
        HashSet<(int, int)> visited = [(0, 0)];
        var santa = (0, 0);
        var roboSanta = (0, 0);
        
        for (var i = 0; i < Directions.Length; i++)
        {
            if (i % 2 == 0)
                santa = UpdatePosition(santa, Directions[i]);
            else
                roboSanta = UpdatePosition(roboSanta, Directions[i]);
            visited.Add(santa);
            visited.Add(roboSanta);
        }
        return visited.Count;
    }
}