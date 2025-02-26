namespace Catalog.Application.Interfaces;

public interface ICategoryRepository : IGenericRepository<Category, Guid>
{
	void ClearGender(Category category);
	Task SetParent(Category category, Guid parentId);
}
