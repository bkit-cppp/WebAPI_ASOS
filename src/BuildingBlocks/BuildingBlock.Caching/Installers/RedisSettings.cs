namespace BuildingBlock.Caching.Installers;

public static class RedisSettings
{
	public static bool Enable { get; } = true;
	public static string ConnectionString { get; } = "localhost:6380";
}

public class Result<T>
{
	public bool Succeeded { get; init; }
	public string ErrorMessage { get; init; }
	public T? Data { get; set; }
	public Result(bool succeeded, string errorMessage)
	{
		Succeeded = succeeded;
		ErrorMessage = errorMessage;
	}

	public static Result<T> Success(T? data)
	{
		var result = new Result<T>(true, string.Empty);
		result.Data = data;

		return result;
	}

	public static Result<T> Failure(string errorMessage)
	{
		return new Result<T>(false, errorMessage);
	}
}
