using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace BuildingBlock.Caching.Services;

public class CacheService : ICacheService
{
	private readonly IDistributedCache _distributedCache;
	private readonly IConnectionMultiplexer _connectionMultiplexer;
	public CacheService(IDistributedCache distributedCache, IConnectionMultiplexer connectionMultiplexer)
	{
		_distributedCache = distributedCache;
		_connectionMultiplexer = connectionMultiplexer;
	}

	public async Task<string> GetCacheResponseAsync(string cacheKey)
	{
		var cacheResponse = await _distributedCache.GetStringAsync(cacheKey);
		return string.IsNullOrEmpty(cacheResponse) ? null : cacheResponse;
	}

	public async Task RemoveCacheResponseAsync(string pattern)
	{
		try
		{
			if (string.IsNullOrWhiteSpace(pattern))
				throw new ArgumentException("value cannot be null or white space");
			var tmp = GetKeyAsync(pattern + "*");
			await foreach (var key in GetKeyAsync(pattern + "*"))
			{
				await _distributedCache.RemoveAsync(key);
			}
		}
		catch (Exception ex)
		{
			Console.WriteLine($"[CacheService] -> RemoveCacheResponseAsync with pattern:{pattern} -> failed , Exception: {ex.Message}");
		}
	}

	private async IAsyncEnumerable<string> GetKeyAsync(string pattern)
	{
		if (string.IsNullOrWhiteSpace(pattern))
			throw new ArgumentException("value cannot be null or white space");
		var tmp = _connectionMultiplexer.GetEndPoints();
		foreach (var endPoint in _connectionMultiplexer.GetEndPoints())
		{
			var server = _connectionMultiplexer.GetServer(endPoint);
			foreach (var key in server.Keys(pattern: pattern))
			{
				yield return key.ToString();
			}
		}
	}

	public async Task SetCacheResponseAsync(string cacheKey, object response, TimeSpan timeOut)
	{
		try
		{
			if (response == null)
				return;
			var serializerResponse = JsonConvert.SerializeObject(response, new JsonSerializerSettings()
			{
				ContractResolver = new CamelCasePropertyNamesContractResolver()
			});

			await _distributedCache.SetStringAsync(cacheKey, serializerResponse, new DistributedCacheEntryOptions
			{
				AbsoluteExpirationRelativeToNow = timeOut
			});
		}
		catch (Exception ex)
		{
			Console.WriteLine($"[CacheService] -> SetCacheResponseAsync with cacheKey:{cacheKey} and object and timeOut: {timeOut} -> failed -> Ex: {ex.Message} ");
		}

	}
}