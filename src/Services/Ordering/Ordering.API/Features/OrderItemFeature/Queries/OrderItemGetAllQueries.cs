using Ordering.API.Features.OrderItemFeature.Dto;

namespace Ordering.API.Features.OrderItemFeature.Queries;

public record OrderItemGetAllQueries(Guid orderId, BaseRequest RequestData) : IQuery<Result<IEnumerable<OrderItemDto>>>;
public class OrderItem_GetAllQueriesHandler : IQueryHandler<OrderItemGetAllQueries, Result<IEnumerable<OrderItemDto>>>
{
    private readonly DataContext _dataContext;
    private readonly IMapper _mapper;
    public OrderItem_GetAllQueriesHandler(DataContext dataContext, IMapper mapper)
    {
        _dataContext = dataContext;
        _mapper = mapper;
    }
    public async Task<Result<IEnumerable<OrderItemDto>>> Handle(OrderItemGetAllQueries request, CancellationToken cancellationToken)
    {
        var orderCol = request.RequestData.OrderCol;
        var orderDir = request.RequestData.OrderDir;

        IEnumerable<OrderItemDto> orderStatus = await _dataContext.OrderItems.OrderedListQuery(orderCol, orderDir)
                                               .Where(s => s.OrderId == request.orderId)
                                               .ProjectTo<OrderItemDto>(_mapper.ConfigurationProvider)
                                               .ToListAsync();

        return Result<IEnumerable<OrderItemDto>>.Success(orderStatus);
    }
}
