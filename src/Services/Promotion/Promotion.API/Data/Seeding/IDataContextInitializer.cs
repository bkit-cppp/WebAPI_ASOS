namespace Promotion.API.Data.Seeding;

public interface IDataContextInitializer
{
	Task SeedAsync();
	Task<int> InitDiscount();
	Task<int> InitDiscountType();
}