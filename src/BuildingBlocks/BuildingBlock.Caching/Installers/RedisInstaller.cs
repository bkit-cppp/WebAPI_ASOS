using BuildingBlock.Caching.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace BuildingBlock.Caching.Installers;

public static class RedisExtension
{
	public static IServiceCollection InstallRedis(this IServiceCollection services, IConfiguration configuration)
	{
		if (!RedisSettings.Enable)
		{
			return services;
		}

		var environment = configuration.GetSection("HostSettings")["Environment"];

		string connectionString = environment == "local" ? 
			RedisSettings.ConnectionString : "distributedcache:6379";

		services.AddSingleton<IConnectionMultiplexer>(_ =>
			ConnectionMultiplexer.Connect(connectionString)
		);

		services.AddStackExchangeRedisCache(option =>
			option.Configuration = connectionString
		);

		services.AddSingleton<ICacheService, CacheService>();

		Console.WriteLine($"*** Redis connected: {connectionString} ***");

		return services;
	}
}