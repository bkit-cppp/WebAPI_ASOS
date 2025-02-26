using Ordering.API.Features.OrderHistoryFeature.Dto;

namespace Ordering.API.Features.OrderHistoryFeature.Queries;

public record OrderHistoryGetAllQueries(BaseRequest RequestData) : IQuery<Result<IEnumerable<OrderHistoryDto>>>;
public class OrderHistoryGetAllQueriesHandler : IQueryHandler<OrderHistoryGetAllQueries, Result<IEnumerable<OrderHistoryDto>>>
{
	private readonly DataContext _dataContext;
	private readonly IMapper _mapper;
	public OrderHistoryGetAllQueriesHandler(IMapper mapper, DataContext dataContext)
	{
		_dataContext = dataContext;
		_mapper = mapper;
	}
	public async Task<Result<IEnumerable<OrderHistoryDto>>> Handle(OrderHistoryGetAllQueries request, CancellationToken cancellationToken)
	{
		var orderCol = request.RequestData.OrderCol;
		var orderDir = request.RequestData.OrderDir;

		IEnumerable<OrderHistoryDto> orderStatus = await _dataContext.OrderHistories.OrderedListQuery(orderCol, orderDir)
											   .ProjectTo<OrderHistoryDto>(_mapper.ConfigurationProvider)
											   .ToListAsync();

		return Result<IEnumerable<OrderHistoryDto>>.Success(orderStatus);
	}
}
