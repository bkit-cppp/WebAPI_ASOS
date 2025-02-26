using Ordering.API.Features.OrderFeature.Dto;

namespace Ordering.API.Features.OrderFeature.Commands;

public record OrderUpdateCommand(OrderUpdateInfoRequest data) : ICommand<Result<OrderDto>>;
public class OrderUpdateCommandHandler : ICommandHandler<OrderUpdateCommand, Result<OrderDto>>
{
    private readonly DataContext _dataContext;
    private readonly IMapper _mapper;
    public OrderUpdateCommandHandler(DataContext dataContext, IMapper mapper)
    {
        _dataContext = dataContext;
        _mapper = mapper;
    }

    public async Task<Result<OrderDto>> Handle(OrderUpdateCommand request, CancellationToken cancellationToken)
    {
        var order = await _dataContext.Orders.FirstOrDefaultAsync(x => x.Id == request.data.Id, cancellationToken);

        if (order == null)
        {
            throw new ApplicationException("Order not found");
        }

        order.ReceiverName = request.data.ReceiverName;
        order.Email = request.data.Email;
        order.Phone = request.data.Phone;
        order.Address = request.data.Address;
        order.ModifiedDate = DateTime.Now;
        order.ModifiedUser = request.data.ModifiedUser;

        _dataContext.Orders.Update(order);
        await _dataContext.SaveChangesAsync();

        return Result<OrderDto>.Success(_mapper.Map<OrderDto>(order));
    }
}
