using BuildingBlock.Core.Result;
using BuildingBlock.Logging;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Net;

namespace BuildingBlock.Installers;

public class ExceptionMiddleware
{
	private readonly RequestDelegate _next;
	private readonly ILoggerService _loggerService;
	public ExceptionMiddleware(RequestDelegate next, ILoggerService loggerService)
	{
		_next = next;
		_loggerService = loggerService;
	}

	public async Task Invoke(HttpContext context)
	{
		try
		{
			await _next(context);
		}
		catch (Exception ex)
		{
			_loggerService.WriteErrorLog(ex, functionName: context.Request.Path.Value ?? "");
			await HandleExceptionAsync(context, ex);
		}
	}

	private static Task HandleExceptionAsync(HttpContext context, Exception exception)
	{
		context.Response.ContentType = "application/json";
		context.Response.StatusCode = (int)HttpStatusCode.OK;

		var response = Result<string>.Failure(exception.Message);
		var jsonResponse = JsonConvert.SerializeObject(response);
		jsonResponse = jsonResponse.Replace("Succeeded", "succeeded");
		jsonResponse = jsonResponse.Replace("ErrorMessage", "errorMessage");
		jsonResponse = jsonResponse.Replace("Data", "data");
		return context.Response.WriteAsync(jsonResponse);
	}
}