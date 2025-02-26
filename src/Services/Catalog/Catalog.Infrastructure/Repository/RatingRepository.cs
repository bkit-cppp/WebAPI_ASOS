using BuildingBlock.Caching.Services;

namespace Catalog.Infrastructure.Repository;

public class RatingRepository : GenericRepository<Rating, Guid>, IRatingRepository
{
	public RatingRepository(DataContext context) : base(context)
	{
		_context = context;
	}
}
