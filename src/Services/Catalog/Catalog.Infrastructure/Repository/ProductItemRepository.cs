namespace Catalog.Infrastructure.Repository;

public class ProductItemRepository : GenericRepository<ProductItem, Guid>, IProductItemRepository
{
	public ProductItemRepository(DataContext context) : base(context)
	{
		_context = context;
	}
}
