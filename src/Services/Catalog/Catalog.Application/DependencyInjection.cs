using BuildingBlock.Grpc;
using BuildingBlock.Grpc.Settings;
using BuildingBlock.Installers;
using BuildingBlock.Messaging.Installers;
using Catalog.Application.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Catalog.Application;

public static class DependencyInjection
{
	public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
	{
		var assembly = Assembly.GetExecutingAssembly();
		services.AddAutoMapper(assembly);
		services.InstallMediatR(assembly);
		services.AddMessagingService(assembly);
		services.InstallGrpc(configuration, new ProtoSetting()
		{
			Identity = true
		});
		return services;
	}

	public static WebApplication UseGrpcRouting(this WebApplication app)
	{
		app.MapGrpcService<CatalogService>();
		return app;
	}
}