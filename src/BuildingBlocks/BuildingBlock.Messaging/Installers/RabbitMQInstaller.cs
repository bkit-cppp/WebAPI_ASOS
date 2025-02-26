using BuildingBlock.Messaging.Abstractions;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace BuildingBlock.Messaging.Installers;

public static class DependencyInjection
{
	public static IServiceCollection AddMessagingService(this IServiceCollection services, Assembly? assembly = null)
	{
		services.AddMassTransit(bus =>
		{
			bus.SetKebabCaseEndpointNameFormatter();

			if (assembly != null)
				bus.AddConsumers(assembly);

			bus.UsingRabbitMq((context, cfg) =>
			{
				cfg.ConfigureEndpoints(context);
				
				/*cfg.Host(new Uri(RabbitMQSettings.Host), h =>
				{
					h.Username(RabbitMQSettings.Username);
					h.Password(RabbitMQSettings.Password);
				});*/
			});
		});
		services.AddTransient<IEventBus, EventBus>();

		Console.WriteLine($"*** RabbitMQ connected ***");

		return services;
	}
}