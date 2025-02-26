namespace Catalog.Infrastructure.Repository;

public class SizeCategoryRepository : GenericRepository<SizeCategory, Guid>, ISizeCategoryRepository
{
	public SizeCategoryRepository(DataContext context) : base(context)
	{
		_context = context;
	}
}
