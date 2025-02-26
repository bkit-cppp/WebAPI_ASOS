namespace BuildingBlock.Utilities;

public static class ConverterHelper
{
	public static List<T> SplitStringToList<T>(string input)
	{
		if (string.IsNullOrEmpty(input))
		{
			return new List<T>();
		}

		return input
			.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
			.Select(value => (T)Convert.ChangeType(value.Trim(), typeof(T)))
			.ToList();
	}
}
