using BuildingBlock.Grpc.Protos;
using BuildingBlock.Grpc.Routers;
using BuildingBlock.Grpc.Services;
using BuildingBlock.Grpc.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
namespace BuildingBlock.Grpc;

public static class DependencyInjection
{
	public static IServiceCollection InstallGrpc(
		this IServiceCollection services, IConfiguration configuration, ProtoSetting setting)
	{
		var environment = configuration.GetSection("HostSettings")["Environment"];

		if (string.IsNullOrEmpty(environment))
		{
			Console.WriteLine("*** [Grpc Injection] - environment null ***");
			return services;
		}

		services.AddGrpc();

		void ConfigureGrpcClient<TClient>(string localHost, string dockerHost) where TClient : class
		{
			var host = environment == "local" ? localHost : dockerHost;
			services.AddGrpcClient<TClient>(options =>
			{
				options.Address = new Uri(host);
			})
			.ConfigurePrimaryHttpMessageHandler(() =>
			{
				var handler = new HttpClientHandler
				{
					ServerCertificateCustomValidationCallback =
					HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
				};

				return handler;
			});
		}

		if (setting.Identity == true)
		{
			ConfigureGrpcClient<IdentityGrpc.IdentityGrpcClient>(LocalRouter.Identity, DockerRouter.Identity);
			services.AddSingleton<IdentityGrpcService>();
		}

		if (setting.Catalog == true)
		{
			ConfigureGrpcClient<CatalogGrpc.CatalogGrpcClient>(LocalRouter.Catalog, DockerRouter.Catalog);
			services.AddSingleton<CatalogGrpcService>();
		}

		if (setting.Promotion == true)
		{
			ConfigureGrpcClient<PromotionGrpc.PromotionGrpcClient>(LocalRouter.Promotion, DockerRouter.Promotion);
			services.AddSingleton<PromotionGrpcService>();
		}

		if (setting.Basket == true)
		{
			ConfigureGrpcClient<BasketGrpc.BasketGrpcClient>(LocalRouter.Basket, DockerRouter.Basket);
			services.AddSingleton<BasketGrpcService>();
		}

		Console.WriteLine($"*** Grpc installed in [{environment}] ***");

		return services;
	}
}
