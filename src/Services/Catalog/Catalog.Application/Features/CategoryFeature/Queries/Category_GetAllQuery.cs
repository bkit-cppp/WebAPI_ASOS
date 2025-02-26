using BuildingBlock.Utilities;
using Catalog.Application.Features.CategoryFeature.Dto;
using Catalog.Application.Models.CategoryModel;

namespace Catalog.Application.Features.CategoryFeature.Queries;

public record Category_GetAllQuery(CategoryGetAllRequest RequestData) : IQuery<Result<IEnumerable<CategoryDto>>>;
public class Category_GetAllQueryHandler : IQueryHandler<Category_GetAllQuery, Result<IEnumerable<CategoryDto>>>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;

	public Category_GetAllQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
		_mapper = mapper;
	}

	public async Task<Result<IEnumerable<CategoryDto>>> Handle(Category_GetAllQuery request, CancellationToken cancellationToken)
	{
		var orderCol = request.RequestData.OrderCol;
		var orderDir = request.RequestData.OrderDir;

		var query = _unitOfWork.Categories.Queryable()
							   .OrderedListQuery(orderCol, orderDir)
							   .ProjectTo<CategoryDto>(_mapper.ConfigurationProvider);

		if (!StringHelper.GuidIsNull(request.RequestData.ParentId))
		{
			query = query.Where(s => s.ParentId == request.RequestData.ParentId!.Value);
		}
		else
		{
			query = query.Where(s => s.ParentId == null);
		}

		return Result<IEnumerable<CategoryDto>>.Success(await query.ToListAsync());
	}
}