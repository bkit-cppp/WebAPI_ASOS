using BuildingBlock.Core.Paging;
using Catalog.Application.Features.SizeFeature.Dto;

namespace Catalog.Application.Features.SizeFeature.Queries;

public record Size_GetPaginationQuery(PaginationRequest RequestData) : IQuery<Result<PaginatedList<SizeDto>>>;
public class Size_GetPaginationQueryHandler : IQueryHandler<Size_GetPaginationQuery, Result<PaginatedList<SizeDto>>>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;

	public Size_GetPaginationQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
		_mapper = mapper;
	}

	public async Task<Result<PaginatedList<SizeDto>>> Handle(Size_GetPaginationQuery request, CancellationToken cancellationToken)
	{
		var orderCol = request.RequestData.OrderCol;
		var orderDir = request.RequestData.OrderDir;

		var query = _unitOfWork.Sizes.Queryable()
							   .OrderedListQuery(orderCol, orderDir)
							   .ProjectTo<SizeDto>(_mapper.ConfigurationProvider)
							   .AsNoTracking();

		if (!string.IsNullOrEmpty(request.RequestData.TextSearch))
		{
			query = query.Where(s => s.Name.Contains(request.RequestData.TextSearch));
		}

		var paging = await query.PaginatedListAsync(request.RequestData.PageIndex, request.RequestData.PageSize);
		return Result<PaginatedList<SizeDto>>.Success(paging);
	}
}