using Ordering.API.Features.OrderStatusFeature.Dto;

namespace Ordering.API.Features.OrderStatusFeature.Queries;

public record OrderStatus_GetByIdQuery(string Id) : IQuery<Result<OrderStatusDto>>;
public class OrderStatus_GetByIdQueryHandler : IQueryHandler<OrderStatus_GetByIdQuery, Result<OrderStatusDto>>
{
    private readonly DataContext _dataContext;
    private readonly IMapper _mapper;
    public OrderStatus_GetByIdQueryHandler(DataContext dataContext, IMapper mapper)
    {
        _dataContext = dataContext;
        _mapper = mapper;
    }

    public async Task<Result<OrderStatusDto>> Handle(OrderStatus_GetByIdQuery request, CancellationToken cancellationToken)
    {
        var  orderStatusDto = await _dataContext.OrderStatus.Where(s => s.Id == request.Id)
                              .ProjectTo<OrderStatusDto>(_mapper.ConfigurationProvider)
                              .FirstOrDefaultAsync();

        return Result<OrderStatusDto>.Success(orderStatusDto);
    }
}

