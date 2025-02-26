namespace Basket.API.Utilities;

public static class CacheHelper
{
	public static string GetCacheKey(Guid id)
	{
		return $"cart:{id}";
	}
}
