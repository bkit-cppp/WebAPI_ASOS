namespace BuildingBlock.Grpc.Routers;

public static class LocalRouter
{
	public static string Identity { get; private set; } = "https://localhost:6001";
	public static string Catalog { get; private set; } = "https://localhost:6002";
	public static string Basket { get; private set; } = "https://localhost:6003";
	public static string Ordering { get; private set; } = "https://localhost:6004";
	public static string Promotion { get; private set; } = "https://localhost:6005";
	public static string Event { get; private set; } = "https://localhost:6006";
	public static string Storage { get; private set; } = "https://localhost:6007";
}
