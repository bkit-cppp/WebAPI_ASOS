namespace Ordering.API.Data.Seeding;

public interface IDataContextInitializer
{
	Task SeedAsync();
	Task<int> InitOrderStatus();
}
