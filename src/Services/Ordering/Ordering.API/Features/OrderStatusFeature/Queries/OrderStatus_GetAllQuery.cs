using Ordering.API.Features.OrderStatusFeature.Dto;

namespace Ordering.API.Features.OrderStatusFeature.Queries;

public record OrderStatus_GetAllQuery(BaseRequest RequestData) : IQuery<Result<IEnumerable<OrderStatusDto>>>;
public class OrderStatus_GetAllQueryHandler : IQueryHandler<OrderStatus_GetAllQuery, Result<IEnumerable<OrderStatusDto>>>
{
	private readonly DataContext _context;
	private readonly IMapper _mapper;

	public OrderStatus_GetAllQueryHandler(IMapper mapper, DataContext context)
	{
		_context = context;
		_mapper = mapper;
	}

	public async Task<Result<IEnumerable<OrderStatusDto>>> Handle(OrderStatus_GetAllQuery request, CancellationToken cancellationToken)
	{
		IEnumerable<OrderStatusDto> orderStatus = await _context.OrderStatus.OrderBy(s => s.Sort)
											   .ProjectTo<OrderStatusDto>(_mapper.ConfigurationProvider)
											   .ToListAsync();

		return Result<IEnumerable<OrderStatusDto>>.Success(orderStatus);
	}
}
