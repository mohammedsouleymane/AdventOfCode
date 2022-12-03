using System.Linq;

namespace AdventOfCode2022;

public static class Day03
{
	private static readonly List<string> Rucksacks = FileReader.GetData("/day03/in.txt");
	private const int Lower = 96;
	private const int Upper = 38;

	public static int SumOfDuplicates => CalSum(DuplicatedItems());
	public static int SumPrioritiesOfBadgeItems => CalSum(BadgeItems());

	public static IEnumerable<char> DuplicatedItems()
	{
		var distinctCompartments = Rucksacks.Select(x => x[0..Mid(x)].Distinct().ToStr() +  x[Mid(x)..].Distinct().ToStr());
		return distinctCompartments.Select(x => x.GroupBy(c=> c).First(y=> y.Count() > 1).Key);
	}

	public static List<char> BadgeItems()
	{
		var badgeItems = new List<char>();
		for (var i = 0; i < Rucksacks.Count; i+=3)
		{
			var group = Rucksacks.Skip(i).Take(3).Select(x=> x.Distinct().ToStr());
			var badgeItem = string.Join("",group).GroupBy(x=> x).First( y=> y.Count() > 2).Key;
			badgeItems.Add(badgeItem);
		}

		return badgeItems;
	}

	private static int Mid (string x) { return x.Length/2; }

	private static int CalSum(IEnumerable<char> ls)
	{
		return ls.Sum(x => char.IsLower(x) ? x - Lower : x - Upper);
	}

	private static string ToStr(this IEnumerable<char> chars)
	{
		return string.Join("", chars);
	}
}