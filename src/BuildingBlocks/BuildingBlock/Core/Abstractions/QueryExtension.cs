using BuildingBlock.Core.Paging;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Reflection;

namespace BuildingBlock.Core.Abstractions;

public static class QueryExtension
{
	public static Task<PaginatedList<TDestination>> PaginatedListAsync<TDestination>(this IQueryable<TDestination> queryable, int pageNumber, int pageSize) where TDestination : class
	=> PaginatedList<TDestination>.CreateAsync(queryable.AsNoTracking(), pageNumber, pageSize);

	public static IQueryable<TDestination> OrderedListQuery<TDestination>(this IQueryable<TDestination> queryable, string? orderCol, string? orderDir) where TDestination : class
	{
		if (string.IsNullOrEmpty(orderCol))
		{
			return queryable;
		}

		if (orderDir != "asc" && orderDir != "desc")
		{
			return queryable;
		}

		var property = typeof(TDestination).GetProperty(orderCol, BindingFlags.Public | BindingFlags.Instance);
		if (property == null)
		{
			return queryable;
		}

		var parameter = Expression.Parameter(typeof(TDestination), "x");
		var propertyAccess = Expression.MakeMemberAccess(parameter, property);
		var orderByExpression = Expression.Lambda(propertyAccess, parameter);

		var methodName = orderDir.Equals("asc", StringComparison.OrdinalIgnoreCase) ? "OrderBy" : "OrderByDescending";

		var resultExpression = Expression.Call(
			typeof(Queryable),
			methodName,
			new Type[] { typeof(TDestination), property.PropertyType },
			queryable.Expression,
			orderByExpression
		);

		return queryable.Provider.CreateQuery<TDestination>(resultExpression);
	}
}
