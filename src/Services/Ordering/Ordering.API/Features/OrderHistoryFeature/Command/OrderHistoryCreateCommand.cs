using Ordering.API.Features.OrderHistoryFeature.Dto;

namespace Ordering.API.Features.OrderHistoryFeature.Command;

public record OrderHistoryCreateCommand(OrderHistoryAddOrUpdate RequestData) : ICommand<Result<OrderHistoryDto>>;
public class OrderHistoryCreateCommandHandler : ICommandHandler<OrderHistoryCreateCommand, Result<OrderHistoryDto>>
{
	private readonly DataContext _context;
	private readonly IMapper _mapper;
	public OrderHistoryCreateCommandHandler(DataContext context, IMapper mapper)
	{
		_context = context;
		_mapper = mapper;
	}
	public async Task<Result<OrderHistoryDto>> Handle(OrderHistoryCreateCommand request, CancellationToken cancellationToken)
	{
		var order = await _context.Orders.FindAsync(request.RequestData.OrderId);
		if (order == null)
		{
			throw new ApplicationException($"Order not found: {request.RequestData.OrderId}");
		}

		var orderHistory = new OrderHistory()
		{
			//Status = request.RequestData.Status,
			Order = order,
			OrderId = order.Id,
			CreatedUser = request.RequestData.CreatedUser,
			ModifiedUser = request.RequestData.CreatedUser
		};

		_context.OrderHistories.Add(orderHistory);

		int rows = await _context.SaveChangesAsync();
		if (rows == 0)
		{
			throw new ApplicationException("Failed to create discount.");
		}

		return Result<OrderHistoryDto>.Success(_mapper.Map<OrderHistoryDto>(orderHistory));
	}
}
