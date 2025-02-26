namespace Catalog.Application.Commons.Interfaces;

public interface IDataContextInitializer
{
	Task SeedAsync();
	Task InitProduct();
	Task InitGender();
	Task InitBrand();
	Task InitCategory();
	Task InitColor();
}
