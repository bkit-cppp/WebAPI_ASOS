using Ordering.API.Features.OrderItemFeature.Dto;

namespace Ordering.API.Features.OrderItemFeature.Queries;

public record OrderItemGetByIdQueries(Guid Id) : IQuery<Result<OrderItemDto>>;
public class OrderItem_GetByIdQueriesHandler : IQueryHandler<OrderItemGetByIdQueries, Result<OrderItemDto>>
{
    private readonly DataContext _dataContext;
    private readonly IMapper _mapper;
    public OrderItem_GetByIdQueriesHandler(DataContext dataContext, IMapper mapper)
    {
        _dataContext = dataContext;
        _mapper = mapper;
    }
    public async Task<Result<OrderItemDto>> Handle(OrderItemGetByIdQueries request, CancellationToken cancellationToken)
    {
        var orderItemDto = await _dataContext.OrderItems.Where(s => s.Id == request.Id)
                             .ProjectTo<OrderItemDto>(_mapper.ConfigurationProvider)
                             .FirstOrDefaultAsync();

        return Result<OrderItemDto>.Success(orderItemDto);
    }
}
