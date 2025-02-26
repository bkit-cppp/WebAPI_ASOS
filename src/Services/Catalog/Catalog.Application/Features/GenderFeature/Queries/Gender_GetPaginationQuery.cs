using BuildingBlock.Core.Paging;
using Catalog.Application.Features.GenderFeature.Dto;

namespace Catalog.Application.Features.GenderFeature.Queries;

public record Gender_GetPaginationQuery(PaginationRequest RequestData) : IQuery<Result<PaginatedList<GenderDto>>>;
public class Gender_GetPaginationQueryHandler : IQueryHandler<Gender_GetPaginationQuery, Result<PaginatedList<GenderDto>>>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;

	public Gender_GetPaginationQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
		_mapper = mapper;
	}

	public async Task<Result<PaginatedList<GenderDto>>> Handle(Gender_GetPaginationQuery request, CancellationToken cancellationToken)
	{
		var orderCol = request.RequestData.OrderCol;
		var orderDir = request.RequestData.OrderDir;

		var query = _unitOfWork.Genders.Queryable()
							   .OrderedListQuery(orderCol, orderDir)
							   .ProjectTo<GenderDto>(_mapper.ConfigurationProvider)
							   .AsNoTracking();

		if (!string.IsNullOrEmpty(request.RequestData.TextSearch))
		{
			query = query.Where(s => s.Name.Contains(request.RequestData.TextSearch));
		}

		var paging = await query.PaginatedListAsync(request.RequestData.PageIndex, request.RequestData.PageSize);
		return Result<PaginatedList<GenderDto>>.Success(paging);
	}
}