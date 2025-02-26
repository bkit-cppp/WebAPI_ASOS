using BuildingBlock.Core.Constants;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace BuildingBlock.Installers;

public static class AuthenticationExtension
{
	public static IServiceCollection InstallAuthentication(this IServiceCollection services)
	{
		services.AddAuthentication(opt =>
		{
			opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
			opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
			opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
		}).AddJwtBearer(options =>
		{
			options.RequireHttpsMetadata = false;
			options.SaveToken = true;
			options.TokenValidationParameters = new TokenValidationParameters()
			{
				ValidateIssuer = true,
				ValidIssuer = JWTConstant.ValidIssuer,
				ValidateAudience = true,
				ValidAudience = JWTConstant.ValidAudience,
				ValidateLifetime = true,
				ValidateIssuerSigningKey = true,
				ClockSkew = System.TimeSpan.Zero,
				IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JWTConstant.Secret))
			};
		});
		services.AddAuthorization();

		return services;
	}
}
