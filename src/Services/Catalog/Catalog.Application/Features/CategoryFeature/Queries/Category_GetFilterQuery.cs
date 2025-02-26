using Catalog.Application.Features.CategoryFeature.Dto;

namespace Catalog.Application.Features.CategoryFeature.Queries;
public record Category_GetFilterQuery(FilterRequest RequestData) : IQuery<Result<IEnumerable<CategoryDto>>>;
public class Category_GetFilterQueryHandler : IQueryHandler<Category_GetFilterQuery, Result<IEnumerable<CategoryDto>>>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;

	public Category_GetFilterQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
		_mapper = mapper;
	}

	public async Task<Result<IEnumerable<CategoryDto>>> Handle(Category_GetFilterQuery request, CancellationToken cancellationToken)
	{
		var orderCol = request.RequestData.OrderCol;
		var orderDir = request.RequestData.OrderDir;

		var query = _unitOfWork.Categories.Queryable().OrderedListQuery(orderCol, orderDir)
							.ProjectTo<CategoryDto>(_mapper.ConfigurationProvider)
							.AsNoTracking();

		if (!string.IsNullOrEmpty(request.RequestData.TextSearch))
		{
			query = query.Where(s => s.Name.Contains(request.RequestData.TextSearch));
		}

		if (request.RequestData.Skip != null)
		{
			query = query.Skip(request.RequestData.Skip.Value);
		}

		if (request.RequestData.TotalRecord != null)
		{
			query = query.Take(request.RequestData.TotalRecord.Value);
		}

		return Result<IEnumerable<CategoryDto>>.Success(await query.ToListAsync());
	}
}