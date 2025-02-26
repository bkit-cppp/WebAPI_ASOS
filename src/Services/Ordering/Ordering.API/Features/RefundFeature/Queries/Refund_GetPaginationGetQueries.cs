using Ordering.API.Features.RefundFeature.Dtos;

namespace Ordering.API.Features.RefundFeature.Queries;

public record Refund_GetPaginationGetQueries(PaginationRequest RequestData) : IQuery<Result<PaginatedList<RefundDto>>>;
public class Refund_GetPaginationGetQueriesHandler : IQueryHandler<Refund_GetPaginationGetQueries, Result<PaginatedList<RefundDto>>>
{
	private readonly IMapper _mapper;
	private readonly DataContext _dataContext;
	public Refund_GetPaginationGetQueriesHandler(IMapper mapper, DataContext dataContext)
	{
		_mapper = mapper;
		_dataContext = dataContext;
	}

	public async Task<Result<PaginatedList<RefundDto>>> Handle(Refund_GetPaginationGetQueries request, CancellationToken cancellationToken)
	{
		var orderCol = request.RequestData.OrderCol;
		var orderDir = request.RequestData.OrderDir;

		var query = _dataContext.Refunds.OrderedListQuery(orderCol, orderDir)
							.ProjectTo<RefundDto>(_mapper.ConfigurationProvider)
							.AsNoTracking();

		var paging = await query.PaginatedListAsync(request.RequestData.PageIndex, request.RequestData.PageSize);
		return Result<PaginatedList<RefundDto>>.Success(paging);
	}
}
