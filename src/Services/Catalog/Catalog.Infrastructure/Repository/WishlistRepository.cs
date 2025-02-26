namespace Catalog.Infrastructure.Repository;

public class WishlistRepository : GenericRepository<WishList, Guid>, IWishlistRepository
{
	public WishlistRepository(DataContext context) : base(context)
	{
		_context = context;
	}
}