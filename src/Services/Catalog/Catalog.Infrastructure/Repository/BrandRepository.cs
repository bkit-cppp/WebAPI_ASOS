namespace Catalog.Infrastructure.Repository;

public class BrandRepository : GenericRepository<Brand, Guid>, IBrandRepository
{
	public BrandRepository(DataContext context) : base(context)
	{
		_context = context;
	}
}
