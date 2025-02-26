using Ordering.API.Features.OrderStatusFeature.Dto;

namespace Ordering.API.Features.OrderStatusFeature.Queries;

public record OrderStatus_GetFilterQuery(FilterRequest RequestData) : IQuery<Result<IEnumerable<OrderStatusDto>>>;
public class OrderStatus_GetFilterQueryHandle : IQueryHandler<OrderStatus_GetFilterQuery, Result<IEnumerable<OrderStatusDto>>>
{
	private readonly IMapper _mapper;
	private readonly DataContext _dataContext;
	public OrderStatus_GetFilterQueryHandle(IMapper mapper, DataContext dataContext)
	{
		_mapper = mapper;
		_dataContext = dataContext;
	}

	public async Task<Result<IEnumerable<OrderStatusDto>>> Handle(OrderStatus_GetFilterQuery request, CancellationToken cancellationToken)
	{
		var query = _dataContext.OrderStatus.OrderBy(s => s.Sort)
                            .ProjectTo<OrderStatusDto>(_mapper.ConfigurationProvider)
							.AsNoTracking();

		if (request.RequestData.Skip != null)
		{
			query = query.Skip(request.RequestData.Skip.Value);
		}

		if (request.RequestData.TotalRecord != null)
		{
			query = query.Take(request.RequestData.TotalRecord.Value);
		}

		return Result<IEnumerable<OrderStatusDto>>.Success(await query.ToListAsync());
	}
}

