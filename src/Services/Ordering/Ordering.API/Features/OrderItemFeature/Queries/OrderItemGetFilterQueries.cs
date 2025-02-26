using Ordering.API.Features.OrderItemFeature.Dto;

namespace Ordering.API.Features.OrderItemFeature.Queries;

public record OrderItemGetFilterQueries(Guid orderId, FilterRequest RequestData) : IQuery<Result<IEnumerable<OrderItemDto>>>;
public class OrderItem_GetFilterQueryHandler : IQueryHandler<OrderItemGetFilterQueries, Result<IEnumerable<OrderItemDto>>>
{
	private readonly IMapper _mapper;
	private readonly DataContext _dataContext;
	public OrderItem_GetFilterQueryHandler(DataContext dataContext, IMapper mapper)
	{
		_mapper = mapper;
		_dataContext = dataContext;
	}
	public async Task<Result<IEnumerable<OrderItemDto>>> Handle(OrderItemGetFilterQueries request, CancellationToken cancellationToken)
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

		if (request.RequestData.Skip != null)
		{
			query = query.Skip(request.RequestData.Skip.Value);
		}

		if (request.RequestData.TotalRecord != null)
		{
			query = query.Take(request.RequestData.TotalRecord.Value);
		}

		return Result<IEnumerable<OrderItemDto>>.Success(await query.ToListAsync());
	}
}
