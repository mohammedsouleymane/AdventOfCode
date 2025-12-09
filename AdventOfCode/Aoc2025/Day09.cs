using System.Numerics;

namespace Aoc2025;

public class Day09
{
    private static List<(long, long)>? RedTiles = Util.ReadFile("/day09/input").Select(x=> x.Split(',').Select(long.Parse).ToArray()).Select(x=> (x[0], x[1])).ToList();
    static readonly List<((long x,long y),(long x,long y))> VerticalEdges = RedTiles.GroupBy(x => x.Item1).Select(x => x.Order())
        .Select(group => (group.First(), group.Last())).ToList();
    static readonly List<((long x,long y),(long x,long y))>  HorizontalEdges = RedTiles.GroupBy(x => x.Item2).Select(x => x.Order())
        .Select(group => (group.First(), group.Last())).ToList();
    public static long BiggestRectangle()
    {
        var area = long.MinValue;
        for (var i = 0; i < RedTiles.Count; i++)
        {
            var (l, r) = RedTiles[i];
            for (var j = i+1; j < RedTiles.Count; j++)
            {
                var (l1, r1) = RedTiles[j];
                var size = (Math.Abs(l - l1) + 1) * (Math.Abs(r - r1) + 1);
                area = Math.Max(area, size);
            }
        }

        return area;
    }

    private static bool Intersect((long,long) xs, (long,long) ys, (long x, long y) s, (long x,long y) e)
    {
        var (minX, maxX) = xs;
        var (minY, maxY) = ys;
        if (s.x <= minX || s.x >= maxX) return false;
        return (s.y <= minY && minY < e.y) || (s.y < maxY && maxY <= e.y);
    }

    public static long LargestAreaOnlyRG()
    {

        var area = long.MinValue;
        for (var i = 0; i < RedTiles.Count; i++)
        {
                var (l, r) = RedTiles[i];
                for (var j = i+1; j < RedTiles.Count; j++)
                {
                    var (l1, r1) = RedTiles[j];
                    var maxX = Math.Max(l1, l);
                    var minX = Math.Min(l1, l);
                    var maxY = Math.Max(r, r1);
                    var minY = Math.Min(r, r1);
                    var intersects = false;

                    foreach (var (s, e) in VerticalEdges)
                    {
                            if (s.x <= minX || s.x >= maxX) continue;
                            if ((s.y > minY || minY >= e.y) && (s.y >= maxY || maxY > e.y)) continue;
                            intersects = true;
                            break;
                    }
                    if(intersects) continue;
                    foreach (var (s, e) in HorizontalEdges)
                    {
                        if (s.y <= minY || s.y >= maxY) continue;

                        if ((s.x > minX || minX >= e.x) && (s.x >= maxX || maxX > e.x)) continue;
                        intersects = true;
                        break;
                    } 
                    if(intersects) continue;
                    var size = (Math.Abs(l - l1) + 1) * (Math.Abs(r - r1) + 1);
                    area = Math.Max(area, size);
                }
        }
        return area;
    }
    
} 