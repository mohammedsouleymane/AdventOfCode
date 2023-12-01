using System.Text.Json;
using System.Text.Json.Nodes;

namespace Aoc2022;

// inspired by https://github.com/encse/adventofcode/blob/master/2022/Day13/Solution.cs
public static class Day13
{
    private static readonly List<string> Packets = Util.ReadFile("/day13/input");

    public static int DecoderKey()
    {
        Packets.AddRange(new List<string>{"[[2]]","[[6]]"});
        var pairs = Packets.Where(x => x != "").Select(ConvertToJsonNode).ToList();
        pairs.Sort(CompareJsons);
        var six = pairs.Select((x, i) => (x, i)).First(x => x.x!.ToJsonString() == "[[6]]").i + 1;
        var two = pairs.Select((x, i) => (x, i)).First(x => x.x!.ToJsonString()  ==  "[[2]]").i + 1;
        return six * two;
    }
    public static int GetOrderedPairs()
    {
        var pairs = Packets.Where(x => x != "").Select(ConvertToJsonNode).Chunk(2).ToArray();
        var orderedPairs = new List<int>();
        for (var j = 0; j < pairs.Length; j++)
        {
            if(CompareJsons(pairs[j][0], pairs[j][1]) < 0)
                orderedPairs.Add(j+1);
        }
        return orderedPairs.Sum();
    }

    private static JsonNode? ConvertToJsonNode(string packet)
    {
        return JsonSerializer.Deserialize<JsonNode>(packet);
    }

    private static int CompareJsons(JsonNode? lElement,JsonNode? rElement)
    {
        if (lElement is JsonValue && rElement is JsonValue)
            return int.Parse(lElement.ToJsonString()) - int.Parse(rElement.ToJsonString());
        
        var lList = lElement as JsonArray ?? new JsonArray((int)lElement);
        var rList = rElement as JsonArray ?? new JsonArray((int)rElement);
        return lList.Zip(rList).Select(p => CompareJsons(p.First, p.Second))
            .FirstOrDefault(c => c != 0, lList.Count - rList.Count);
    }
}