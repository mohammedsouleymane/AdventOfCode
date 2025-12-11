namespace Aoc2025;

public static class Day11
{
    private static readonly List<string>  Devices = Util.ReadFile("/day11/input");

    private static readonly Dictionary<string, long> Cache = new();

    private static long CountFresh(string device, Dictionary<string, string[]> devices, string end = "out")
    {
        Cache.Clear();
        return Count(device, devices, end);
    }

    private static long Count(string device, Dictionary<string, string[]> devices, string end )
    {
        if (Cache.TryGetValue(device, out var count)) return count;
        if (end != "out" && device == "out") return 0;
        if (devices[device].Contains(end) )
            return 1;
        
        var sum =  devices[device].Sum(d => Count(d, devices,end));
        Cache.Add(device, sum);
        return sum;
    }

    public static long Paths(bool partOne = true)
    {
        Dictionary<string, string[]> devices = [];
        foreach (var parsed in Devices.Select(device => device.Split(": ")))
        {
            devices.Add(parsed[0], parsed[1].Split(" "));
        }
        
        if (partOne) // part one
            return CountFresh("you", devices); // default from you to start
        //another solution is to keep the same structure as part 1 but cache device and i (counter for seeing fft and dac) Dict<(string,int),long> 
        //i++ when device is fft or dac and only return 1 when i == 2
        
        //used solution: divide into sub problems it looks like fft always comes before dac
        return CountFresh("svr", devices, "fft") * // we count the paths from svr to fft
               CountFresh("fft", devices, "dac") * // then from fft to dac
               CountFresh("dac", devices); // dac to out by multiplying them we get the count
    }
}