using System.Text.Json.Serialization;

namespace Aoc2024;

public static class Day04
{
    private static readonly char[,] WordSearch = Util.ReadFile("/day04/input").ToCharMatrix();
    public static int Search()
    {
        List<(int,int)> directions = [(0, 1), (0, -1), (1, 0), (-1, 0), (1,1),(-1,1),(1,-1),(-1,-1)];
        var count = 0;
        for (var i = 0; i < WordSearch.GetLength(0); i++)
        {
            for (var j = 0; j < WordSearch.GetLength(1); j++)
            {
                if (WordSearch.GetELementAt((i, j)) != 'X') continue;
                count += directions.Count(x => Find("X", (i, j), x, "XMAS",3));
            }
        }

        return count;
    }


    public static int SearchMas()
    {
        List<(int i ,int j)> directions = [(1,1),(-1,1),(1,-1),(-1,-1)];
        var positionOfA = new List<(int, int)>();
        for (var i = 0; i < WordSearch.GetLength(0); i++)
        {
            for (var j = 0; j < WordSearch.GetLength(1); j++)
            {
                if (WordSearch.GetELementAt((i, j)) != 'M') continue;
                foreach (var direction in directions)
                {
                    if (Find("M", (i, j), direction, "MAS", 2))
                    {
                        positionOfA.Add((i +direction.i, j + direction.j)); // saves this A positions 
                    }
                }
            }
        }

        return positionOfA.GroupBy(x => x).Count(x => x.Count() > 1); // groups by the coordinates and checks where if has move than 1
    }
    
    private static bool Find(string word,(int i, int j) start, (int i, int j) direction, string toFind, int end)
    {
        var (x, y) = direction;
        if (!WordSearch.InBounds((start.i + x * end , start.j + y * end)))
            return false;
        for (var i = 0; i < end; i++)
        {
            start = (start.i + direction.i, start.j + direction.j);
            var letter = WordSearch[start.i, start.j];
            word += letter;
            if (!toFind.StartsWith(word))
                return false;
        }
        return true;
    }
}