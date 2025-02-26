using Microsoft.Extensions.DependencyInjection;

namespace BuildingBlock.Installers;

public static class CORSExtension
{
	public static IServiceCollection InstallCORS(this IServiceCollection services)
	{
		services.AddCors(options =>
		{
			options.AddDefaultPolicy(
				builder =>
				{
					/*builder.AllowAnyOrigin()
							.AllowAnyMethod()
							.AllowAnyHeader()
							.WithExposedHeaders("Content-Disposition");*/

					builder.WithOrigins("http://localhost:5173")
					   .AllowAnyMethod()
					   .AllowAnyHeader()
					   .AllowCredentials()
					   .WithExposedHeaders("Content-Disposition");
				});
		});

		return services;
	}
}
