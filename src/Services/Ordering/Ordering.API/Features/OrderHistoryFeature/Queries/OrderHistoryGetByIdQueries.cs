using Ordering.API.Features.OrderHistoryFeature.Dto;

namespace Ordering.API.Features.OrderHistoryFeature.Queries;

public record OrderHistoryGetByIdQueries(Guid Id) : IQuery<Result<OrderHistoryDto>>;
public class OrderHistoryGetByIdQueriesHandler : IQueryHandler<OrderHistoryGetByIdQueries, Result<OrderHistoryDto>>
{
    private readonly DataContext _dataContext;
    private readonly IMapper _mapper;
    public OrderHistoryGetByIdQueriesHandler(DataContext dataContext, IMapper mapper)
    {
        _dataContext = dataContext;
        _mapper = mapper;
    }
    public async Task<Result<OrderHistoryDto>> Handle(OrderHistoryGetByIdQueries request, CancellationToken cancellationToken)
    {
        var orderHistoryDto = await _dataContext.OrderHistories.Where(s => s.Id == request.Id)
                              .ProjectTo<OrderHistoryDto>(_mapper.ConfigurationProvider)
                              .FirstOrDefaultAsync();

        return Result<OrderHistoryDto>.Success(orderHistoryDto);
    }
}
