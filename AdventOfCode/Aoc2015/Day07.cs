namespace Aoc2015;

public class Day07
{

    public static int Signal(string wire = "a", bool two = false)
    {
        var instructionss = Util.ReadFile("/day07/input");
        var dictionary = new Dictionary<string, int>();
        while (!dictionary.ContainsKey(wire))
        {
            var instructions = instructionss.First();
            var splitInstruction = instructions.Split(" -> ");
            var mapping = splitInstruction[1];
            if (Util.DigitsOnly(splitInstruction[0]))
            {
                dictionary.Add(mapping, mapping == "b" && two ? Signal() : int.Parse(splitInstruction[0]));
                instructionss.RemoveAt(0);
                continue;
            }

            if (splitInstruction[0].StartsWith("NOT"))
            {
                var key = splitInstruction[0].Split(" ")[1];
                if (dictionary.ContainsKey(splitInstruction[0].Split(" ")[1]))
                    dictionary.Add(mapping, 65535 - dictionary[key]);
                else
                    instructionss.Add(instructions);
                instructionss.RemoveAt(0);
                continue;
            }

            if (splitInstruction[0].Contains("AND"))
            {
                var values = splitInstruction[0].Split(" AND ").Order().ToArray();
                if (values.All(dictionary.ContainsKey))
                    dictionary.Add(mapping, dictionary[values[0]] & dictionary[values[1]]);
                else if (Util.DigitsOnly(values[0]) && dictionary.TryGetValue(values[1], out int value))
                    dictionary.Add(mapping, int.Parse(values[0]) & value);
                else
                    instructionss.Add(instructions);
                instructionss.RemoveAt(0);
                continue;
            }

            if (splitInstruction[0].Contains("OR"))
            {
                var values = splitInstruction[0].Split(" OR ").Order().ToArray();
                if (values.All(dictionary.ContainsKey))
                    dictionary.Add(mapping, dictionary[values[0]] | dictionary[values[1]]);
                else if (Util.DigitsOnly(values[0]) && dictionary.TryGetValue(values[1], out int value))
                    dictionary.Add(mapping, int.Parse(values[0]) | value);
                else
                    instructionss.Add(instructions);
                instructionss.RemoveAt(0);
                continue;
            }

            if (splitInstruction[0].Contains("LSHIFT"))
            {
                var values = splitInstruction[0].Split(" LSHIFT ");
                if (dictionary.ContainsKey(values[0]))
                    dictionary.Add(mapping, dictionary[values[0]] << int.Parse(values[1]));
                else
                    instructionss.Add(instructions);
                instructionss.RemoveAt(0);
                continue;
            }

            if (splitInstruction[0].Contains("RSHIFT"))
            {
                var values = splitInstruction[0].Split(" RSHIFT ");
                if (dictionary.ContainsKey(values[0]))
                    dictionary.Add(mapping, dictionary[values[0]] >> int.Parse(values[1]));
                else
                    instructionss.Add(instructions);
                instructionss.RemoveAt(0);
                continue;
            }

            if (dictionary.ContainsKey(splitInstruction[0]))
                dictionary.Add(mapping, dictionary[splitInstruction[0]]);
            else
                instructionss.Add(instructions);
            instructionss.RemoveAt(0);
        }

        return dictionary[wire];
    }
}