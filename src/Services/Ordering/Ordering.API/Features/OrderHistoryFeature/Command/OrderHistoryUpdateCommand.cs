using Ordering.API.Features.OrderHistoryFeature.Dto;

namespace Ordering.API.Features.OrderHistoryFeature.Command;

public record OrderHistoryUpdateCommand(OrderHistoryAddOrUpdate RequestData) : ICommand<Result<OrderHistoryDto>>;
public class OrderHistoryUpdateCommandHandler : ICommandHandler<OrderHistoryUpdateCommand, Result<OrderHistoryDto>>
{
	private readonly DataContext _context;
	private readonly IMapper _mapper;
	public OrderHistoryUpdateCommandHandler(DataContext context, IMapper mapper)
	{
		_context = context;
		_mapper = mapper;
	}
	public async Task<Result<OrderHistoryDto>> Handle(OrderHistoryUpdateCommand request, CancellationToken cancellationToken)
	{
		var orderHistory = await _context.OrderHistories.FindAsync(request.RequestData.Id);

		if (orderHistory == null)
		{
			throw new ApplicationException("Order history not found");
		}

		//orderHistory.Status = request.RequestData.Status;
		orderHistory.ModifiedDate = DateTime.Now;
		orderHistory.ModifiedUser = request.RequestData.ModifiedUser;

		_context.OrderHistories.Update(orderHistory);
		int rows = await _context.SaveChangesAsync();

		if (rows == 0)
		{
			throw new ApplicationException("Failed to create discount.");
		}

		return Result<OrderHistoryDto>.Success(_mapper.Map<OrderHistoryDto>(orderHistory));
	}
}
