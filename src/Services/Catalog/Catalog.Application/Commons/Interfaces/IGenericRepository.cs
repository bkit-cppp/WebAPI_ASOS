namespace Catalog.Application.Commons.Interfaces;

public interface IGenericRepository<TEntity, in TKey> where TEntity : class
{
	// ===================================== QUERIES ======================================= 
	IQueryable<TEntity> Queryable();
	Task<TEntity?> FindAsync(TKey id, bool isThrow = false);
	Task<TEntity?> FindSlugAsync(string slug, bool isThrow = false);
	Task<List<TEntity>> GetAllAsync();
	Task<TEntity?> IsSlugUnique(string slug, bool isThrow = false);
	Task<List<TEntity>> FindByIds(IEnumerable<TKey> ids, bool isThrow = false);

	// ===================================== COMMANDS ======================================= 
	bool Add(TEntity entity, Guid? user = null);
	bool Delete(TEntity entity, Guid? user = null);
	bool Update(TEntity entity, Guid? user = null);
	bool SoftDelete(TEntity entity, Guid? user = null);

	bool SoftDeleteRange(List<TEntity> entities, Guid? user = null);
	bool AddRange(List<TEntity> entities, Guid? user = null);
	bool DeleteRange(List<TEntity> entities, Guid? user = null);
	bool UpdateRange(List<TEntity> entities, Guid? user = null);

}