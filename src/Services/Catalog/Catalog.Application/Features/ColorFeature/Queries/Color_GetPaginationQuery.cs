using BuildingBlock.Core.Paging;
using Catalog.Application.Features.ColorFeature.Dto;

namespace Catalog.Application.Features.ColorFeature.Queries;
public record Color_GetPaginationQuery(PaginationRequest RequestData) : IQuery<Result<PaginatedList<ColorDto>>>;
public class Color_GetPaginationQueryHandler : IQueryHandler<Color_GetPaginationQuery, Result<PaginatedList<ColorDto>>>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;

	public Color_GetPaginationQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
		_mapper = mapper;
	}

	public async Task<Result<PaginatedList<ColorDto>>> Handle(Color_GetPaginationQuery request, CancellationToken cancellationToken)
	{
		var orderCol = request.RequestData.OrderCol;
		var orderDir = request.RequestData.OrderDir;

		var query = _unitOfWork.Colors.Queryable()
							   .OrderedListQuery(orderCol, orderDir)
							   .ProjectTo<ColorDto>(_mapper.ConfigurationProvider)
							   .AsNoTracking();

		if (!string.IsNullOrEmpty(request.RequestData.TextSearch))
		{
			query = query.Where(s => s.Name.Contains(request.RequestData.TextSearch));
		}

		var paging = await query.PaginatedListAsync(request.RequestData.PageIndex, request.RequestData.PageSize);
		return Result<PaginatedList<ColorDto>>.Success(paging);
	}
}
