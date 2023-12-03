namespace Aoc2023;

public static class Day03
{
    private static readonly List<string> Schematic = Util.ReadFile("/day03/input");
    private static readonly int LineLength = Schematic.First().Length;
    private static readonly Dictionary<(int i, int j), List<int>> DictionaryOfStars = new();

    private static bool CheckIfSymbol(string s)
    {
        return s.Length != 0 && s.Any(x => !char.IsDigit(x) && x != '.');
    }

    private static bool AddToDictionary(string character, int i, int j, string number)
    {
        if (character != "*") return true;
        var key = (i, j);
        var num = int.Parse(number);
        if (!DictionaryOfStars.ContainsKey(key))
            DictionaryOfStars.Add(key, new List<int> { num });
        else if (DictionaryOfStars[key].Count < 2)
            DictionaryOfStars[key].Add(num);
        else
            DictionaryOfStars.Remove(key);
        return true;
    }

    private static bool LookForAdjacentSymbols(string number, int index)
    {
        var start = Schematic[index].IndexOf(number, StringComparison.Ordinal);
        var end = start + number.Length;
        if (end < LineLength - 1)
        {
            var rightCharacter = Schematic[index][ end].ToString(); // check right 
            if (CheckIfSymbol(rightCharacter))
                return AddToDictionary(rightCharacter, index, end, number);
        }

        if (start > 0)
        {
            var leftCharacter = Schematic[index][--start].ToString(); // check left 
            if (CheckIfSymbol(leftCharacter))
            {
                AddToDictionary(leftCharacter, index, start, number); // part two
                return true;
            }
        }
        
        if (end < LineLength - 1) ++end;
        if (index < Schematic.Count - 1) // check under and left and right diags 
        {
            var charactersUnderNumber = Schematic[index + 1][start .. end];
            if (CheckIfSymbol(charactersUnderNumber))
            {
                for (var i = 0; i < charactersUnderNumber.Length; i++)
                {
                    AddToDictionary(charactersUnderNumber[i].ToString(), index + 1, start + i, number); // part two
                }
                return true;
            }
        }

        if (index <= 0) return false; // check up and left and right diags

        var charactersAboveNumber = Schematic[index - 1][start .. end];
        if (!CheckIfSymbol(charactersAboveNumber)) return false;
        for (var i = 0; i < charactersAboveNumber.Length; i++)
        {
            AddToDictionary(charactersAboveNumber[i].ToString(), index - 1, start + i, number); // part two
        }

        return true;
    }

    public static int One()
    {
        var sum = 0;
        for (var i = 0; i < Schematic.Count; i++)
        {
            var symbolsToSplit = Schematic[i].Where(x => !char.IsDigit(x)).Distinct(); // get all symbols in input
            var temp = Schematic[i];
            temp = symbolsToSplit.Aggregate(temp,
                (current, c) =>
                    current.Replace(c,
                        '.')); // to remove cases like 768*721 by -> * to . 768.721 and then split on . happens for all symbols

            var numbers = temp.Split(".").Where(x => x.Length > 0);
            foreach (var number in numbers)
            {
                if (number.Length == 0) continue;
                if (LookForAdjacentSymbols(number, i))
                    sum += int.Parse(number);

                var start = Schematic[i].IndexOf(number, StringComparison.Ordinal);
                Schematic[i] =
                    Schematic[i]
                        .ReplaceAt(start, number.Length,
                            "".PadLeft(number.Length, '.')); // removes number when finished with it;
            }
        }

        return sum;
    }

    public static int Two => DictionaryOfStars.Select(k => k.Value).Where(x => x.Count == 2)
        .Select(x => x.First() * x.Last()).Sum();
}