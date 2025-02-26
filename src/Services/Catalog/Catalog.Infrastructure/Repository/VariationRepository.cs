namespace Catalog.Infrastructure.Repository;

public class VariationRepository : GenericRepository<Variation, Guid>, IVariationRepository
{
	public VariationRepository(DataContext context) : base(context)
	{
		_context = context;
	}
}
