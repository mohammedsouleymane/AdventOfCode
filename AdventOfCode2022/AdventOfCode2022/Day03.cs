namespace AdventOfCode2022;

public static class Day03
{
	private static readonly List<string> Rucksacks = FileReader.GetData("/day03/in.txt");

	private const int Lower = 96;
	private const int Upper = 38;


	public static IEnumerable<char> DuplicatedItems()
	{
		var distinctCompartments = Rucksacks.Select(x =>new string(x[0..Mid(x)].Distinct().ToArray()) + new string(x[Mid(x)..].Distinct().ToArray()));
		return distinctCompartments.Select(x => x.GroupBy(c=> c).First(y=> y.Count() > 1).Key);
	}

	public static List<char> BadgeItem()
	{
		var badgeItems = new List<char>();
		for (var i = 0; i < Rucksacks.Count; i+=3)
		{
			var group = Rucksacks.Skip(i).Take(3).Select(x=> new string (x.Distinct().ToArray()));
			var badgeItem = string.Join("",group).GroupBy(x=> x).Where( y=> y.Count() > 2);
			badgeItems.Add(badgeItem.First().Key);
		}

		return badgeItems;
	}

	public static int Sum => CalSum(DuplicatedItems());

	public static int SumBadgeItems => CalSum(BadgeItem());
	private static int Mid (string x) { return x.Length/2; }

	private static int CalSum(IEnumerable<char> ls)
	{
		return ls.Sum(x => char.IsLower(x) ? x - Lower : x - Upper);
	}
}