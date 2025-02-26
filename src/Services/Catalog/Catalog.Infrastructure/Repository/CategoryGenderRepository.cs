namespace Catalog.Infrastructure.Repository;

public class CategoryGenderRepository : GenericRepository<CategoryGender, Guid>, ICategoryGenderRepository
{
	public CategoryGenderRepository(DataContext context) : base(context)
	{
		_context = context;
	}
}
