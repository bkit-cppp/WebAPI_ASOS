namespace Catalog.Infrastructure.Repository;

public class ColorRepository : GenericRepository<Color, Guid>, IColorRepository
{
	public ColorRepository(DataContext context) : base(context)
	{
		_context = context;
	}
}
