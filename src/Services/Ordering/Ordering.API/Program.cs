using BuildingBlock.Grpc;
using BuildingBlock.Installers;
using Ordering.API.Data.Seeding;
using Ordering.API.Implements;
using Ordering.API.Interfaces;
using Ordering.API.Settings;
using System.Reflection;
using BuildingBlock.Grpc.Settings;
using Microsoft.EntityFrameworkCore.Diagnostics;
using BuildingBlock.Messaging.Installers;

namespace Ordering.API
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			var assembly = Assembly.GetExecutingAssembly();
			var builder = WebApplication.CreateBuilder(args);
			builder.Services.AddControllers();
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.Configure<VnpaySettings>(builder.Configuration.GetSection("VnpaySettings"));

			#region BuildingBlock
			builder.InstallSerilog();
			builder.Services.InstallGrpc(builder.Configuration, new ProtoSetting()
			{
				Identity = true,
				Basket = true
			});
			builder.Services.InstallSwagger("v1", "API Ordering");
			builder.Services.InstallCORS();
			builder.Services.InstallAuthentication();
			builder.Services.InstallMediatR(assembly);
            builder.Services.AddAutoMapper(assembly);
			builder.Services.AddMessagingService(assembly);
			#endregion

			#region DataContext
			//var cnStr = builder.Configuration.GetConnectionString("Database");
			//AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
			//builder.Services.AddDbContext<DataContext>(options => options.UseNpgsql(cnStr));
			var connectionString = builder.Configuration.GetConnectionString("Database");
            builder.Services.AddDbContext<DataContext>((sp, options) =>
            {
                options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
                options.UseSqlServer(connectionString);
                options.UseSqlServer(connectionString, sqlServerOptions =>
                {
                    sqlServerOptions.EnableRetryOnFailure(
                        maxRetryCount: 5, // Số lần thử lại tối đa
                        maxRetryDelay: TimeSpan.FromSeconds(10), // Thời gian trễ giữa các lần thử
                        errorNumbersToAdd: null // Các mã lỗi bổ sung (để mặc định nếu không có mã cụ thể)
                    );
                });
            });
            #endregion

            builder.Services.AddTransient<IDataContextInitializer, DataContextInitializer>();
			builder.Services.AddScoped<IPaymentService, PaymentService>();
			builder.Services.AddScoped<IOrderHistoryService, OrderHistoryService>();

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
