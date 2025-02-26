using Ordering.API.Features.OrderHistoryFeature.Dto;

namespace Ordering.API.Features.OrderHistoryFeature.Queries;

public record OrderHistoryGetFilterQueries(OrderHistoryFilterRequest RequestData) : IQuery<Result<IEnumerable<OrderHistoryDto>>>;
public class OrderHistoryGetFilterQueriesHandler : IQueryHandler<OrderHistoryGetFilterQueries, Result<IEnumerable<OrderHistoryDto>>>
{
	private readonly IMapper _mapper;
	private readonly DataContext _dataContext;
	public OrderHistoryGetFilterQueriesHandler(IMapper mapper, DataContext dataContext)
	{
		_dataContext = dataContext;
		_mapper = mapper;

	}
	public async Task<Result<IEnumerable<OrderHistoryDto>>> Handle(OrderHistoryGetFilterQueries request, CancellationToken cancellationToken)
	{
		var orderCol = request.RequestData.OrderCol;
		var orderDir = request.RequestData.OrderDir;

		var query = _dataContext.OrderHistories.OrderedListQuery(orderCol, orderDir)
							.ProjectTo<OrderHistoryDto>(_mapper.ConfigurationProvider)
							.AsNoTracking();

		if (!StringHelper.GuidIsNull(request.RequestData.OrderId))
		{
			query = query.Where(s => s.OrderId == request.RequestData.OrderId);
		}

		if (request.RequestData.Skip != null)
		{
			query = query.Skip(request.RequestData.Skip.Value);
		}

		if (request.RequestData.TotalRecord != null)
		{
			query = query.Take(request.RequestData.TotalRecord.Value);
		}

		return Result<IEnumerable<OrderHistoryDto>>.Success(await query.ToListAsync());
	}
}

