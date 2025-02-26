using BuildingBlock.Installers;
namespace Event.API
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);
			builder.InstallSerilog();
			builder.Services.AddControllers();
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.InstallSwagger("v1", "API Event");
			builder.Services.InstallCORS();
			builder.Services.InstallAuthentication();

			var app = builder.Build();

			app.UseSwaggerService();
			app.UseCors();
			app.UseHttpsRedirection();
			app.UseRouting();
			app.UseAuthentication();
			app.UseAuthorization();
			app.UseMiddleware<ExceptionMiddleware>();
			//app.MigrationAutoUpdate<DataContext>();
			app.MapControllers();
			app.Run();
		}
	}
}
