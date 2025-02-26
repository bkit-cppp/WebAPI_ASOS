using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace BuildingBlock.Installers;

public static class SwaggerExtension
{
	public static IServiceCollection InstallSwagger(this IServiceCollection services,string version,string title)
	{
		services.AddSwaggerGen(swa =>
		{
			swa.SwaggerDoc(version, new OpenApiInfo
			{
				Title = title,
				Version = version
			});

			var securitySchema = new OpenApiSecurityScheme
			{
				Description =
					"JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
				Name = "Authorization",
				In = ParameterLocation.Header,
				Type = SecuritySchemeType.Http,
				Scheme = JwtBearerDefaults.AuthenticationScheme,
				Reference = new OpenApiReference
				{
					Type = ReferenceType.SecurityScheme,
					Id = JwtBearerDefaults.AuthenticationScheme
				}
			};
			swa.AddSecurityDefinition("Bearer", securitySchema);

			var securityRequirement = new OpenApiSecurityRequirement
				{
					{ securitySchema, new[] { "Bearer" } }
				};
			swa.AddSecurityRequirement(securityRequirement);
		});

		return services;
	}

	public static WebApplication UseSwaggerService(this WebApplication app)
	{
		app.UseSwagger();
		app.UseSwaggerUI();

		return app;
	}
}
