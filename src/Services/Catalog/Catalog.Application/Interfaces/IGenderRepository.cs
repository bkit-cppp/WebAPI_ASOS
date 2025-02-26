namespace Catalog.Application.Interfaces;

public interface IGenderRepository : IGenericRepository<Gender, Guid>
{
	Task<List<Gender>> GetBatchIds(List<Guid> ids);
}
