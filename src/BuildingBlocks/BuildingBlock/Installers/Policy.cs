using BuildingBlock.Constants;
using BuildingBlock.Core.Policies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace BuildingBlock.Installers;

public static class PolicyExtension
{
	public static IServiceCollection InstallPolicy(this IServiceCollection services)
	{
		services.AddAuthorization(options =>
		{
			List<string> roles = new List<string>()
			{
				RoleConstant.ADMIN,
				RoleConstant.CUSTOMER
			};
			options.AddPolicy("RoleBasePolicy", policy =>
				policy.Requirements.Add(new RolePolicyRequirement(roles)));
		});

		// Đăng ký AuthorizationHandler
		services.AddSingleton<IAuthorizationHandler, RoleRequirementHandler>();

		return services;
	}
}
