using System.Numerics;
using System.Runtime.InteropServices.JavaScript;

namespace Aoc2023;

public static class Day18
{
    private static readonly IEnumerable<string[]> _lines = Util.ReadFile("/day18/input").Select(x => x.Split(" "));
    private static long ShoelaceAndPick(IList<(long y, long x)> points, long boundary)
    {
        long sum = 0;
        for (var i = 0; i < points.Count; i++)
        {
            if(i == 0)
                sum +=  points[i].x * (points[^1].y - points[(i + 1) % points.Count].y);
            else
                sum +=  points[i].x * (points[i - 1].y - points[(i + 1) % points.Count].y);
        }
        
        return boundary + (Math.Abs(sum) / 2 )- (boundary / 2) + 1;
    }
    public static long One()
    {
        var coordinates = new HashSet<(long y, long x)>{(0,0)};
        long boundary = 0;
        foreach (var line in  _lines)
        {
            var count = int.Parse(line[1]);
            boundary += count;
            switch (line[0])
            {
                case "L":
                    coordinates.Add( (coordinates.Last().y, coordinates.Last().x - count ));
                    break;
                case "R":
                    coordinates.Add((coordinates.Last().y, coordinates.Last().x + count));
                    break;
                case "U":
                    coordinates.Add( (coordinates.Last().y - count, coordinates.Last().x));
                    break;
                case "D":
                    coordinates.Add( (coordinates.Last().y + count, coordinates.Last().x));
                    break;
            }
        }
        return ShoelaceAndPick(coordinates.ToArray(), boundary);
    }
    
    public static long Two()
    {
        var coordinates = new HashSet<(long y, long x)>{(0,0)};
        var boundary = 0;
        foreach (var line in  _lines)
        {
            var count = Convert.ToInt32(line[2][2..7], 16);
            boundary += count;
            var l = "RDLU"[int.Parse(line[2][^2].ToString())].ToString();
            switch (l)
            {
                case "L":
                    coordinates.Add( (coordinates.Last().y, coordinates.Last().x - count ));
                    break;
                case "R":
                    coordinates.Add((coordinates.Last().y, coordinates.Last().x + count));
                    break;
                case "U":
                    coordinates.Add( (coordinates.Last().y - count, coordinates.Last().x));
                    break;
                case "D":
                    coordinates.Add( (coordinates.Last().y + count, coordinates.Last().x));
                    break;
            }
        }

        return ShoelaceAndPick(coordinates.ToArray(), boundary);
    }
}