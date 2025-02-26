namespace BuildingBlock.Messaging.Installers;

public static class RabbitMQSettings
{
	public static string Host { get; } = "amqp://host-queue:15672";
	public static string Username { get; } = "guest";
	public static string Password { get; } = "guest";
}
