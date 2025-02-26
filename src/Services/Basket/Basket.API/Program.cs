using BuildingBlock.Grpc.Settings;
using BuildingBlock.Grpc;
using BuildingBlock.Installers;
using System.Reflection;
using BuildingBlock.Messaging.Installers;

namespace Basket.API
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var assembly = Assembly.GetExecutingAssembly();
			var builder = WebApplication.CreateBuilder(args);

			builder.Services.AddControllers();
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddScoped<ICartService, CartService>();

			#region BuildingBlock
			builder.InstallSerilog();
			builder.Services.InstallCORS();
			builder.Services.InstallRedis(builder.Configuration);
			builder.Services.InstallAuthentication();
			builder.Services.AddAutoMapper(assembly);
			builder.Services.InstallMediatR(assembly);
			builder.Services.InstallSwagger("v1", "API Basket");
			builder.Services.AddMessagingService(assembly);
			builder.Services.InstallGrpc(builder.Configuration, new ProtoSetting()
			{
				Identity = true,
				Catalog = true,
				Promotion = true
			});
			#endregion

			var app = builder.Build();
			app.UseSwaggerService();
			app.UseCors();
			app.UseHttpsRedirection();
			app.UseRouting();
			app.MapGrpcService<BasketService>();
			app.UseAuthentication();
			app.UseAuthorization();
			app.UseMiddleware<ExceptionMiddleware>();
			app.MapControllers();
			app.Run();
		}
	}
}
