namespace Catalog.Infrastructure.Repository;

public class SizeRepository : GenericRepository<Size, Guid>, ISizeRepository
{
	public SizeRepository(DataContext context) : base(context)
	{
		_context = context;
	}
}
