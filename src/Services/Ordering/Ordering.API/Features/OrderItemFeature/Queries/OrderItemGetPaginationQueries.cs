using Ordering.API.Features.OrderItemFeature.Dto;

namespace Ordering.API.Features.OrderItemFeature.Queries;

public record OrderItemGetPaginationQueries(Guid orderId, PaginationRequest RequestData) : IQuery<Result<PaginatedList<OrderItemDto>>>;
public class OrderItem_GetPaginationQueriesHandler : IQueryHandler<OrderItemGetPaginationQueries, Result<PaginatedList<OrderItemDto>>>
{
	private readonly IMapper _mapper;
	private readonly DataContext _dataContext;
	public OrderItem_GetPaginationQueriesHandler(IMapper mapper, DataContext dataContext)
	{
		_mapper = mapper;
		_dataContext = dataContext;
	}
	public async Task<Result<PaginatedList<OrderItemDto>>> Handle(OrderItemGetPaginationQueries request, CancellationToken cancellationToken)
	{
		var orderCol = request.RequestData.OrderCol;
		var orderDir = request.RequestData.OrderDir;

		var query = _dataContext.OrderItems.OrderedListQuery(orderCol, orderDir)
							.Where(s => s.OrderId == request.orderId)
							.ProjectTo<OrderItemDto>(_mapper.ConfigurationProvider)
							.AsNoTracking();

		if (!string.IsNullOrEmpty(request.RequestData.TextSearch))
		{
			query = query.Where(s => s.Name.Contains(request.RequestData.TextSearch));
		}

		var paging = await query.PaginatedListAsync(request.RequestData.PageIndex, request.RequestData.PageSize);
		return Result<PaginatedList<OrderItemDto>>.Success(paging);
	}
}
