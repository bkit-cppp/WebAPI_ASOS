using AutoMapper;
using AutoMapper.QueryableExtensions;
using Ordering.API.Features.OrderHistoryFeature.Dto;

namespace Ordering.API.Features.OrderHistoryFeature.Queries;

public record OrderHistoryGetpaginationQueries(PaginationRequest RequestData) : IQuery<Result<PaginatedList<OrderHistoryDto>>>;
public class OrderHistoryGetPaginationQueriesHandler : IQueryHandler<OrderHistoryGetpaginationQueries, Result<PaginatedList<OrderHistoryDto>>>
{
	private readonly IMapper _mapper;
	private readonly DataContext _dataContext;
	public OrderHistoryGetPaginationQueriesHandler(IMapper mapper, DataContext dataContext)
	{
		_mapper = mapper;
		_dataContext = dataContext;
	}
	public async Task<Result<PaginatedList<OrderHistoryDto>>> Handle(OrderHistoryGetpaginationQueries request, CancellationToken cancellationToken)
	{
		var orderCol = request.RequestData.OrderCol;
		var orderDir = request.RequestData.OrderDir;

		var query = _dataContext.OrderHistories.OrderedListQuery(orderCol, orderDir)
							.ProjectTo<OrderHistoryDto>(_mapper.ConfigurationProvider)
							.AsNoTracking();


		var paging = await query.PaginatedListAsync(request.RequestData.PageIndex, request.RequestData.PageSize);
		return Result<PaginatedList<OrderHistoryDto>>.Success(paging);
	}
}
