using BuildingBlock.Grpc;
using BuildingBlock.Grpc.Settings;
using BuildingBlock.Installers;
using BuildingBlock.Messaging.Installers;
using Identity.API.Data.Seeding;
using Promotion.API.Data;
using Promotion.API.Data.Seeding;
using Promotion.API.Services;
using System.Reflection;

namespace Promotion.API
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			var assembly = Assembly.GetExecutingAssembly();
			var builder = WebApplication.CreateBuilder(args);
			builder.Services.AddControllers().AddJsonOptions(options =>
			{
				options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
			});

			builder.Services.AddControllers();
			builder.Services.AddEndpointsApiExplorer();

            #region BuildingBlock
            builder.InstallSerilog();
			builder.Services.InstallGrpc(builder.Configuration, new ProtoSetting());
			builder.Services.InstallSwagger("v1", "API Promotion");
			builder.Services.InstallCORS();
			builder.Services.InstallAuthentication();
			builder.Services.InstallMediatR(assembly);
			builder.Services.AddAutoMapper(assembly);
			builder.Services.AddMessagingService(assembly);
			#endregion

			#region Service Register
			builder.Services.AddTransient<IDataContextInitializer, DataContextInitializer>();
			#endregion

			#region DataContext
			var cnStr = builder.Configuration.GetConnectionString("Database");
			AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
			builder.Services.AddDbContext<DataContext>(options => options.UseNpgsql(cnStr));
			#endregion

			var app = builder.Build();
			app.UseSwaggerService();
			app.UseCors();
			app.UseHttpsRedirection();
			app.UseRouting();
			app.UseAuthentication();
			app.UseAuthorization();
			app.UseMiddleware<ExceptionMiddleware>();
			app.MapGrpcService<PromotionService>();
			app.MigrationAutoUpdate<DataContext>();
			app.MapControllers();

			using (var scope = app.Services.CreateScope())
			{
				IServiceProvider services = scope.ServiceProvider;
				IDataContextInitializer initializer = services.GetRequiredService<IDataContextInitializer>();
				await initializer.SeedAsync();
				scope.Dispose();
			}

			app.Run();
		}
	}
}
