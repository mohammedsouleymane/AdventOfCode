namespace Utilities;

public static class Util
{
	public static string ReplaceAt(this string str, int index, int length, string replace)
	{
		return str.Remove(index, Math.Min(length, str.Length - index))
			.Insert(index, replace);
	}
	public static List<string> ReadFile(string path)
	{
		path = Directory.GetParent(Directory.GetCurrentDirectory())?.Parent?.Parent + "/input"+path;
		return File.ReadAllLines(path).ToList();
	}

	public static string ToStr<T>(this IEnumerable<T> list, string separator = "")
	{
		return string.Join(separator, list);
	}
	public static T[] GetColumn<T>( this T[,] matrix, int columnNumber)
	{
		return Enumerable.Range(0, matrix.GetLength(0))
			.Select(x => matrix[x, columnNumber])
			.ToArray();
	}

	public static T[] GetRow<T>(this T[,] matrix, int rowNumber)
	{
		return Enumerable.Range(0, matrix.GetLength(1))
			.Select(x => matrix[rowNumber, x]).ToArray();
	}

	public static int[,] ToMatrix(this IEnumerable<string> list)
	{
		var matrix = new int[list.First().Length, list.Count()];
		for (var i = 0; i < matrix.GetLength(0); i++)
		{
			for (var j = 0; j < matrix.GetLength(1); j++)
			{
				matrix[i, j] = int.Parse(list.ToArray()[i][j].ToString());
			}
		}

		return matrix;
	}
	
	public static char[,] ToCharMatrix(this IEnumerable<string> list)
	{
		var matrix = new char[list.First().Length, list.Count()];
		for (var i = 0; i < matrix.GetLength(0); i++)
		{
			for (var j = 0; j < matrix.GetLength(1); j++)
			{
				matrix[i, j] = list.ToArray()[i][j];
			}
		}
		return matrix;
	}
}