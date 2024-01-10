using System.Text.RegularExpressions;

namespace Aoc2023;

public class Day15
{
    private static readonly string[] Lines = Util.ReadFile("/day15/input").ToStr().Split(",");
    
    private static int Calculate(string s)
    {
        var sum = 0;
        foreach (var c in s)
        {
            sum += c;
            sum *= 17;
            sum %= 256;
        }

        return sum;
    }

    public static int One => Lines.Sum(Calculate);

    public static int Two()
    {
        var boxes = new Dictionary<int, List<string>?>();
        foreach (var operation in Lines)
        {
            var box = Calculate( Regex.Replace(operation, "[^a-zA-Z]+", "")) + 1;
            var sign = operation.Contains("=") ? "=" : "-";
            var spl = operation.Split(sign);
            var str = spl.ToStr(" ");
            if (boxes.TryGetValue(box, out var value) && value!.Any(x=> x.Contains(spl[0])) )
            {
                switch (sign)
                {
                    case "-":
                        boxes[box] = value!.Where(x=> !x.Contains(spl[0])).ToList();
                        if (boxes[box]!.Count == 0) boxes.Remove(box); 
                        break;
                    case "=":
                        boxes[box] = value!.Select(x => x.Contains(spl[0]) ? str : x).ToList();
                        break;
                }
            }
            else if(sign == "=")
            {
                if(!boxes.ContainsKey(box)) 
                    boxes.Add(box, new List<string>{str});
                else
                    boxes[box]!.Add(str);
            }
        }
        
        var sum = 0;
        foreach (var (k,v) in boxes)
        {
            sum += boxes[k]!.Select((x, j) => k * (j + 1) * int.Parse(x.Split(" ")[1])).Sum();
        }
        return sum;
    }
}