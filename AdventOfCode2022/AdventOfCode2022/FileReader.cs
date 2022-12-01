namespace AdventOfCode2022;

public static class FileReader
{
	public static List<string> GetData(string path)
	{
		path = Directory.GetParent(Directory.GetCurrentDirectory())?.Parent?.Parent + "/input"+path;
		return File.ReadAllLines(path).ToList();
	}
}