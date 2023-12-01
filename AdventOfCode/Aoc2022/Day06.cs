namespace Aoc2022;

public static class Day06
{
    private static readonly string DatastreamBuffers = Util.ReadFile("/day06/input").First();

    public static readonly int StartOfPacketMarker = DatastreamBuffers.Select((_, i) => i)
        .First(x => DatastreamBuffers[x..(x + 4)].Distinct().Count() == 4) + 4;

    public static readonly int StartOfMessageMarker = DatastreamBuffers.Select((_, i) => i)
        .First(x => DatastreamBuffers[x..(x + 14)].Distinct().Count() == 14) + 14;
}