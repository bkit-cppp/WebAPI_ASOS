using System.Linq.Expressions;

namespace Catalog.Infrastructure.Seedworks;

public class GenericRepository<TEntity, TKey> : IGenericRepository<TEntity, TKey> where TEntity : class
{
    protected DataContext _context;
    protected DbSet<TEntity> _dbSet;

    public GenericRepository(DataContext context)
    {
        _context = context;
        _dbSet = context.Set<TEntity>();
    }

	#region =========== Common ===========
	public IQueryable<TEntity> Queryable()
    {
        IQueryable<TEntity> query = _dbSet;
        return query;
    }

	private void SetModifiedAttribute(TEntity entity, Guid? user = null)
	{
		var modifiedUser = typeof(TEntity).GetProperty("ModifiedUser");
		var modifiedDate = typeof(TEntity).GetProperty("ModifiedDate");

		if (modifiedUser is not null && user is not null)
		{
			modifiedUser.SetValue(entity, user);
		}

		if (modifiedDate is not null)
		{
			modifiedDate.SetValue(entity, DateTime.Now);
		}
	}

	private void SetCreatedAttribute(TEntity entity, Guid? user = null)
	{
		var createdDate = typeof(TEntity).GetProperty("CreatedDate");
		var createdUser = typeof(TEntity).GetProperty("CreatedUser");

		if (createdUser is not null && user is not null)
		{
			createdUser.SetValue(entity, user);
		}

		if (createdDate is not null)
		{
			createdDate.SetValue(entity, DateTime.Now);
		}
	}

	private void SetSoftDeletedAttribute(TEntity entity, Guid? user = null)
	{
		var deleteFlag = typeof(TEntity).GetProperty("DeleteFlag");
		var modifiedDate = typeof(TEntity).GetProperty("ModifiedDate");
		var modifiedUser = typeof(TEntity).GetProperty("ModifiedUser");

		if (deleteFlag is not null)
		{
			deleteFlag.SetValue(entity, true);
		}

		if (modifiedUser is not null && user is not null)
		{
			modifiedUser.SetValue(entity, user);
		}

		if (modifiedDate is not null)
		{
			modifiedDate.SetValue(entity, DateTime.Now);
		}
	}
	#endregion

	#region =========== Command ===========
	public virtual bool Add(TEntity entity, Guid? user = null)
	{
		try
		{
			SetCreatedAttribute(entity, user);
			_dbSet.Add(entity);
			return true;
		}
		catch (Exception ex)
		{
			return false;
		}
	}

	public virtual bool Update(TEntity entity, Guid? user = null)
	{
		try
		{
			SetModifiedAttribute(entity, user);
			_dbSet.Attach(entity);
			_context.Entry(entity).State = EntityState.Modified;
			return true;
		}
		catch (Exception ex)
		{
			return false;
		}
	}

	public virtual bool Delete(TEntity entity, Guid? user = null)
	{
		try
		{
			_dbSet.Remove(entity);
			return true;
		}
		catch (Exception ex)
		{
			return false;
		}
	}

	public virtual bool SoftDelete(TEntity entity, Guid? user = null)
	{
		try
		{
			SetSoftDeletedAttribute(entity, user);
			_dbSet.Attach(entity);
			_context.Entry(entity).State = EntityState.Modified;

			return true;
		}
		catch (Exception ex)
		{
			return false;
		}
	}
	#endregion

	#region =========== Command Range ===========
	public bool SoftDeleteRange(List<TEntity> entities, Guid? user = null)
	{
		try
		{
			foreach (var entity in entities)
			{
				SetSoftDeletedAttribute(entity, user);
			}
			_dbSet.UpdateRange(entities);

			return true;
		}
		catch (Exception ex)
		{
			return false;
		}
	}

	public bool AddRange(List<TEntity> entities, Guid? user = null)
	{
		try
		{
			foreach (var entity in entities)
			{
				SetCreatedAttribute(entity, user);
			}

			_dbSet.AddRange(entities);
			return true;
		}
		catch (Exception ex)
		{
			return false;
		}
	}

	public bool DeleteRange(List<TEntity> entities, Guid? user = null)
	{
		try
		{
			_dbSet.RemoveRange(entities);
			return true;
		}
		catch (Exception ex)
		{
			return false;
		}
	}

	public bool UpdateRange(List<TEntity> entities, Guid? user = null)
	{
		try
		{
			foreach (var entity in entities)
			{
				SetModifiedAttribute(entity, user);
			}

			_dbSet.UpdateRange(entities);
			return true;
		}
		catch (Exception ex)
		{
			return false;
		}
	}
	#endregion

	#region ======== Queries ========
	public virtual async Task<TEntity?> FindAsync(TKey id, bool isThrow = false)
	{
		var entity = await _dbSet.FindAsync(id);
		if (entity == null && isThrow)
		{
			throw new ApplicationException($"{typeof(TEntity).Name} not found: {id}");
		}
		return entity;
	}

	public async Task<List<TEntity>> GetAllAsync()
	{
		var data = await _dbSet.ToListAsync();
		return data;
	}

	public async Task<TEntity?> IsSlugUnique(string slug, bool isThrow = false)
	{
		var slugProperty = typeof(TEntity).GetProperty("Slug");

		if (slugProperty == null)
		{
			throw new ApplicationException($"Entity does not have a column named 'Slug'.");
		}

		var parameter = Expression.Parameter(typeof(TEntity), "e");
		var property = Expression.Property(parameter, slugProperty);
		var value = Expression.Constant(slug);
		var equalExpression = Expression.Equal(property, value);

		var lambda = Expression.Lambda<Func<TEntity, bool>>(equalExpression, parameter);

		var entity = await _dbSet.FirstOrDefaultAsync(lambda);

		if (entity != null && isThrow)
		{
			throw new ApplicationException($"Data with slug '{slug}' is already in use.");
		}

		return entity;
	}

	public async Task<TEntity?> FindSlugAsync(string slug, bool isThrow = false)
	{
		var slugProperty = typeof(TEntity).GetProperty("Slug");

		if (slugProperty == null)
		{
			throw new ApplicationException($"Entity does not have a column named 'Slug'.");
		}

		var parameter = Expression.Parameter(typeof(TEntity), "e");
		var property = Expression.Property(parameter, slugProperty);
		var value = Expression.Constant(slug);
		var equalExpression = Expression.Equal(property, value);

		var lambda = Expression.Lambda<Func<TEntity, bool>>(equalExpression, parameter);

		var entity = await _dbSet.FirstOrDefaultAsync(lambda);

		if (entity == null && isThrow)
		{
			throw new ApplicationException($"Not found data with slug: {slug}");
		}

		return entity;
	}

	public async Task<List<TEntity>> FindByIds(IEnumerable<TKey> ids, bool isThrow = false)
	{
		var idProperty = typeof(TEntity).GetProperty("Id");

		if (idProperty == null)
		{
			throw new ApplicationException($"Entity does not have a column named 'Id'.");
		}

		var parameter = Expression.Parameter(typeof(TEntity), "e");
		var property = Expression.Property(parameter, idProperty);
		var containsMethod = typeof(Enumerable).GetMethods()
			.First(m => m.Name == "Contains" && m.GetParameters().Length == 2)
			.MakeGenericMethod(typeof(TKey));

		var idsConstant = Expression.Constant(ids);
		var containsExpression = Expression.Call(containsMethod, idsConstant, property);
		var lambda = Expression.Lambda<Func<TEntity, bool>>(containsExpression, parameter);
		var entities = await _dbSet.Where(lambda).ToListAsync();

		if (!entities.Any() && isThrow)
		{
			throw new ApplicationException($"Data not found for ids: {string.Join(";", ids)}");
		}

		return entities;
	}
	#endregion

}
