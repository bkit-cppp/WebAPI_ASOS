namespace Catalog.Infrastructure.Repository;

public class GenderRepository : GenericRepository<Gender, Guid>, IGenderRepository
{
	public GenderRepository(DataContext context) : base(context)
	{
		_context = context;
	}

	public async Task<List<Gender>> GetBatchIds(List<Guid> ids)
	{
		return await _context.Genders.Where(s => ids.Contains(s.Id)).ToListAsync();
	}
}
