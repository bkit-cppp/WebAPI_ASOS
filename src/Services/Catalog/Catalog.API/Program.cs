using BuildingBlock.Caching.Installers;
using BuildingBlock.Installers;
using Catalog.Application;
using Catalog.Application.Commons.Interfaces;
using Catalog.Infrastructure;
using Catalog.Infrastructure.Data;
using System.Reflection;

namespace Catalog.API
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);
			builder.Services.AddControllers();
			builder.Services.AddEndpointsApiExplorer();

			#region DependencyInjection
		
			builder.Services.AddApplicationServices(builder.Configuration);
			builder.Services.AddInfrastructureServices(builder.Configuration);
			#endregion

			#region BuildingBlock
			builder.InstallSerilog();
			builder.Services.InstallSwagger("v1", "API Catalog");
			builder.Services.InstallCORS();
            builder.Services.InstallMediatR(Assembly.GetExecutingAssembly());

            builder.Services.InstallAuthentication();
			builder.Services.InstallRedis(builder.Configuration);
			#endregion

			var app = builder.Build();
			app.UseSwaggerService();
			app.UseCors();
			app.UseHttpsRedirection();
			app.UseRouting();
			app.UseAuthentication();
			app.UseAuthorization();
			app.UseMiddleware<ExceptionMiddleware>();
			app.MigrationAutoUpdate<DataContext>();
			app.MapControllers();
			app.UseGrpcRouting();
            using (var scope = app.Services.CreateScope())
            {
                IServiceProvider services = scope.ServiceProvider;
                IDataContextInitializer initializer = services.GetRequiredService<IDataContextInitializer>();
                await initializer.SeedAsync();
            }
            app.Run();
		}
	}
}
