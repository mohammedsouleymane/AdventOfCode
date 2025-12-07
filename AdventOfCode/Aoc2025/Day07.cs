namespace Aoc2025;

public class Day07
{
    private static readonly char[,] Diagram = Util.ReadFile("/day07/input").ToCharMatrix();
    public static int BeamSplits()
    {
        var start = Diagram.Find('S');
        HashSet<(int i, int j)> beams = [start];
        var splits = 0;
        for (var i = 0; i < Diagram.GetLength(0); i++) // goes down by length of grid because that's the max it can go
        {
            var temp = beams.Select(x => x.Add((1, 0))).Where(x => Diagram.InBounds(x)).ToHashSet(); // all active beams move down by 1
            beams.Clear();
            foreach (var beam in temp)
            {
                if (Diagram.GetElementAt(beam) == '^') // if splitter
                {
                    splits++; // splitter counter
                    beams.Add(beam.Add((0, 1))); // add new beam (right)
                    beams.Add(beam.Add((0, -1))); // add new beam (left)
                }
                else
                    beams.Add(beam); // otherwise keep beam
            }
           
        }
        return splits;
    }

    private static readonly Dictionary<(int, int), long> Cache = new();
    public static long Timelines((int i, int j) s)
    {
        
        if (s == (-1, -1))
            s = Diagram.Find('S');
        
        if (!Diagram.InBounds(s)) return 1; // out of bounds = end of timeline
        if (Cache.TryGetValue(s, out var timelines)) return timelines; // already in cache

        long timeline = 0;

        if (Diagram.GetElementAt(s) == '^') // explores other timelines
        {
            timeline += Timelines(s.Add((0, 1)));
            timeline += Timelines(s.Add((0, -1)));
        }
        else
            timeline += Timelines(s.Add((1,0)));
        
        Cache.Add(s, timeline);
        return timeline;
    }
}