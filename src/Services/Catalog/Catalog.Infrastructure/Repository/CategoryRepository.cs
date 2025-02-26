namespace Catalog.Infrastructure.Repository;

public class CategoryRepository : GenericRepository<Category, Guid>, ICategoryRepository
{
	public CategoryRepository(DataContext context) : base(context)
	{
		_context = context;
	}

	public void ClearGender(Category category)
	{
		/*if (category.Genders != null)
		{
			category.Genders.Clear();
		}*/
	}

	public async Task SetParent(Category category, Guid parentId)
	{
		await FindAsync(parentId, true);
		category.ParentId = parentId;
	}
}
