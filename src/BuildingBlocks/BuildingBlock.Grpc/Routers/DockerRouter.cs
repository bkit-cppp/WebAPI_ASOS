namespace BuildingBlock.Grpc.Routers;

public static class DockerRouter
{
	public static string Identity { get; private set; } = "https://identity.api:8081";
	public static string Catalog { get; private set; } = "https://catalog.api:8081";
	public static string Basket { get; private set; } = "https://basket.api:8081";
	public static string Ordering { get; private set; } = "https://ordering.api:8081";
	public static string Promotion { get; private set; } = "https://promotion.api:8081";
	public static string Event { get; private set; } = "https://event.api:8081";
	public static string Storage { get; private set; } = "https://storage.api:8081";
}
