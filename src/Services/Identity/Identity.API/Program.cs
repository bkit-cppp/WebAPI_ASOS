using BuildingBlock.Grpc;
using BuildingBlock.Installers;
using BuildingBlock.Messaging.Installers;
using Identity.API.Data.Seeding;
using Identity.API.Hubs;
using Identity.API.Implementations;
using Identity.API.Interfaces;
using Identity.API.Services;
using Microsoft.AspNetCore.Http.Connections;
using System.Reflection;
using BuildingBlock.Grpc.Settings;
using BuildingBlock.Caching.Installers;

namespace Identity.API
{
	public class Program
	{
		public static string BaseUrl { get; private set; }
		public static async Task Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);
			var environment = builder.Configuration.GetSection("HostSettings")["Environment"];

			builder.Services.AddControllers().AddJsonOptions(options =>
			{
				options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
			});
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSignalR();
			
			#region Service Register
			builder.Services.AddScoped<ITokenService, TokenService>();
			builder.Services.AddScoped<IUserService, UserService>();
			builder.Services.AddScoped<INotificationService, NotificationService>();
			builder.Services.AddTransient<IDataContextInitializer, DataContextInitializer>();
			#endregion

			#region BuildingBlock
			builder.InstallSerilog();
			builder.Services.InstallGrpc(builder.Configuration,new ProtoSetting());
			builder.Services.InstallSwagger("v1", "API Identity");
			builder.Services.InstallCORS();
			builder.Services.InstallAuthentication();
			builder.Services.InstallRedis(builder.Configuration);
			builder.Services.InstallMediatR(Assembly.GetExecutingAssembly());
			builder.Services.AddMessagingService(Assembly.GetExecutingAssembly());
			builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
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
			app.MapControllers();
			app.MapGrpcService<IdentityService>();
			app.MigrationAutoUpdate<DataContext>();

			app.Use(async (context, next) =>
			{
				if(environment == "docker")
				{
					BaseUrl = "https://localhost:7000/identity-service";
				}
				else
				{
					BaseUrl = $"{context.Request.Scheme}://{context.Request.Host}";
				}
				await next.Invoke();
			});

			app.MapHub<NotificationHub>("/notification", options =>
			{
				options.Transports = HttpTransportType.WebSockets;
			});

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
