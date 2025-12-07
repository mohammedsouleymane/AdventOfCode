namespace Aoc2025;

public static class Day04
{
    private static readonly char[,] Grid =  Util.ReadFile("/day04/input").ToCharMatrix();
    private static readonly List<(int, int)> Directions = [(1, 1), (-1, -1), (0, 1), (0, -1), (1, 0), (-1, 0), (1,-1),(-1,1)];
    public static int AccessibleRolls(bool repeat = false)
    {
        List<(int, int)> accessibleRolls = [];
        for (var i = 0; i < Grid.GetLength(0); i++)
        {
            for (var j = 0; j < Grid.GetLength(1); j++)
            {
                if(Grid[i,j] == '.') continue;
                var adjacentRollsCount = Directions
                    .Select(x => x.Add((i, j)))// add all direction to current direction
                    .Count(x => Grid.InBounds(x) && Grid.GetElementAt(x) == '@'); // checks that the position is inbounds and element equals '@
                if (adjacentRollsCount < 4)
                    accessibleRolls.Add((i,j));
            }
        }

        if (!repeat) return accessibleRolls.Count;
        foreach(var (i,j) in accessibleRolls)
        {
            Grid[i, j] = '.';
        }
        return accessibleRolls.Count;
    }

    public static int RepeatProcess()
    {
        
        var accessibleRolls = AccessibleRolls(true);
        var sum = accessibleRolls;
        while (accessibleRolls != 0)
        {
            accessibleRolls = AccessibleRolls(true);
            sum += accessibleRolls;
        }

        return sum;
    }
}