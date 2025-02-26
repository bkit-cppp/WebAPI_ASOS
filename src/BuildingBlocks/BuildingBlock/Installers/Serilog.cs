using BuildingBlock.Logging;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;

namespace BuildingBlock.Installers;

public static class SerilogExtension
{
	public static WebApplicationBuilder InstallSerilog(this WebApplicationBuilder builder)
	{
		Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(builder.Configuration).CreateLogger();
		builder.Logging.ClearProviders();
		builder.Logging.AddSerilog();
		builder.Logging.AddConsole();
		builder.Services.AddTransient<ILoggerService, LoggerService>();

		return builder;
	}
}