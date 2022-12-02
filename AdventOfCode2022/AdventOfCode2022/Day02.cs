using System.Text;

namespace AdventOfCode2022;

public static class Day02
{
	private static List<char> _opponents = new() {'A','B','C'};
	private static List<char> _me = new() { 'X', 'Y', 'Z' };
	private static List<string> rounds = FileReader.GetData("/day02/in.txt");
	private static Dictionary<char, char> wins = new()
	{
		{'X', 'C'},
		{'Y', 'A'},
		{'Z', 'B'}
	};
	private static Dictionary<char, char> winsOpp = new()
	{
		{'A', 'Z'},
		{'B', 'X'},
		{'C', 'Y'}
	};
	public static int PartOne()
	{
		var score = 0;
		foreach (var round in rounds)
		{
			var opp = round[0];
			var me = round[^1];
			score += _me.IndexOf(me) + 1;
			if (wins[me] == opp)
				score += 6;
			else if (_opponents.IndexOf(opp) == _me.IndexOf(me))
				score += 3;
		}
		return score;
	}

	public static int PartTwo()
	{
		var score = 0;
		foreach (var round in rounds)
		{
			 var opp = round[0];
			 var me = round[^1];
			 score += me switch
			 {
				 'Y' => 3 + _opponents.IndexOf(opp) + 1,
				 'Z' => 6 + _me.IndexOf(wins.FirstOrDefault(x => x.Value == opp).Key) + 1,
				 _ => _me.IndexOf(winsOpp[opp]) + 1
			 };
		}
		return score;
	}

}