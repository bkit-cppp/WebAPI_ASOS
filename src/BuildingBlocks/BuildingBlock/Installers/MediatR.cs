using BuildingBlock.Behaviors;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace BuildingBlock.Installers;

public static class MediatRExtension
{
	public static IServiceCollection InstallMediatR(this IServiceCollection services,Assembly assembly)
	{
		services.AddMediatR(config =>
		{
			config.RegisterServicesFromAssembly(assembly);
			config.AddOpenBehavior(typeof(ValidationBehavior<,>));
			//config.AddOpenBehavior(typeof(LoggingBehavior<,>));
		});
		services.AddValidatorsFromAssembly(assembly);

		return services;
	}
}
