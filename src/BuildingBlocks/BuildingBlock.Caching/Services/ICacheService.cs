﻿namespace BuildingBlock.Caching.Services;

public interface ICacheService
{
	Task SetCacheResponseAsync(string cacheKey, object response, TimeSpan timeOut);
	Task<string> GetCacheResponseAsync(string cacheKey);
	Task RemoveCacheResponseAsync(string pattern);
}
