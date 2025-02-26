using Ordering.API.Features.OrderHistoryFeature.Dto;

namespace Ordering.API.Features.OrderFeature.Queries;

public record OrderHistoryGetByOrderIdQuery (Guid OrderId) : IQuery<Result<IEnumerable<OrderHistoryDto>>>;
public class OrderHistoryGetByOrderIdQueryHandler : IQueryHandler<OrderHistoryGetByOrderIdQuery, Result<IEnumerable<OrderHistoryDto>>>
{
    private readonly DataContext _dataContext;
    private readonly IMapper _mapper;

    public OrderHistoryGetByOrderIdQueryHandler(DataContext dataContext, IMapper mapper)
    {
        _dataContext = dataContext;
        _mapper = mapper;
    }
    public async Task<Result<IEnumerable<OrderHistoryDto>>> Handle(OrderHistoryGetByOrderIdQuery request, CancellationToken cancellationToken)
    {
        var orderHistories = await _dataContext.OrderHistories
            .OrderByDescending(s => s.CreatedDate)
            .Where(history => history.OrderId == request.OrderId)
            .ToListAsync(cancellationToken);

		var result = _mapper.Map<IEnumerable<OrderHistoryDto>>(orderHistories);

        return Result<IEnumerable<OrderHistoryDto>>.Success(result);
    }
}
