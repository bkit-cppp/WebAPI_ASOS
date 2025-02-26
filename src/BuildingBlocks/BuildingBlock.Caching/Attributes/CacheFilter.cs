using BuildingBlock.Caching.Installers;
using BuildingBlock.Caching.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System.Text;

namespace BuildingBlock.Caching.Attributes;

public class CacheAttribute : Attribute, IAsyncActionFilter
{
	private readonly int _timeToLiveSeconds;
	private readonly bool _authorize;
	public CacheAttribute(int timeToLiveSeconds = 10, bool authorize = false)
	{
		_timeToLiveSeconds = timeToLiveSeconds;
		_authorize = authorize;
	}

	public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
	{
		try
		{
			if (!RedisSettings.Enable)
			{
				await next();
				return;
			}
			context.HttpContext.Request.EnableBuffering();
			using (StreamReader stream = new StreamReader(context.HttpContext.Request.Body))
			{
				string body = await stream.ReadToEndAsync();

			}

			var cacheKey = generateCacheKeyFromRequest(context.HttpContext.Request, context, _authorize);
			var cacheService = context.HttpContext.RequestServices.GetRequiredService<ICacheService>();
			var cacheResponse = await cacheService.GetCacheResponseAsync(cacheKey);

			if (!string.IsNullOrEmpty(cacheResponse))
			{
				var contentResult = new ContentResult
				{
					Content = cacheResponse,
					ContentType = "application/json",
					StatusCode = 200
				};
				context.Result = contentResult;
				return;
			}

			var excutedContext = await next();
			if (excutedContext.Result is OkObjectResult jsonResult)
			{
				var jsonString = JsonConvert.SerializeObject(jsonResult.Value);
				var resultDynamic = JsonConvert.DeserializeObject<Result<dynamic>>(jsonString);
				if(resultDynamic != null)
				{
					var succeeded = (bool)resultDynamic.Succeeded;
					if (succeeded)
					{
						await cacheService.SetCacheResponseAsync(cacheKey, jsonResult.Value, TimeSpan.FromSeconds(_timeToLiveSeconds));
					}
				}
			}
		}
		catch (Exception ex)
		{
			Console.WriteLine($"[Cache filter] error at {context.HttpContext.Request.Path}");
		}
	}

	private static string generateCacheKeyFromRequest(HttpRequest request, ActionExecutingContext context, bool authorize)
	{
		var keyBuilder = new StringBuilder();
		keyBuilder.Append($"{request.Path}");
		if (authorize)
		{
			string user = context.HttpContext.Items["userId"] as string ?? "";
			if(!string.IsNullOrEmpty(user)) keyBuilder.Append($"|user-{user}");
		}
		foreach (var (key, value) in request.Query.OrderBy(x => x.Key))
		{
			keyBuilder.Append($"|{key}-{value}");
		}
		return keyBuilder.ToString();
	}
}