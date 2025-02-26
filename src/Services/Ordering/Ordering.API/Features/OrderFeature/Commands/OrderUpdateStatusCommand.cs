using BuildingBlock.Messaging.Abstractions;
using BuildingBlock.Messaging.Events;
using Ordering.API.Features.OrderFeature.Dto;

namespace Ordering.API.Features.OrderFeature.Commands;

public record OrderUpdateStatusCommand(UpdateStatusRequest RequestData) : ICommand<Result<OrderDto>>;
public class OrderUpdateStatusCommandHandler : ICommandHandler<OrderUpdateStatusCommand, Result<OrderDto>>
{
	private readonly IOrderHistoryService _historyService;
	private readonly DataContext _context;
	private readonly IMapper _mapper;
	private readonly IEventBus _eventBus;
	public OrderUpdateStatusCommandHandler(DataContext context, IMapper mapper, IEventBus eventBus, IOrderHistoryService historyService)
	{
		_historyService = historyService;
		_context = context;
		_mapper = mapper;
		_eventBus = eventBus;
	}
	public async Task<Result<OrderDto>> Handle(OrderUpdateStatusCommand request, CancellationToken cancellationToken)
	{
		var order = await _context.Orders.Include(s => s.Status)
					.FirstOrDefaultAsync(s => s.Id == request.RequestData.Id);

		if (order == null)
		{
			throw new ApplicationException("Order not found");
		}

		if (order.Status == null || order.StatusId == OrderStatusConstant.Pending)
		{
			throw new ApplicationException("Order status is invalid");
		}

		var status = await _context.OrderStatus.FindAsync(request.RequestData.Status);

		if (status == null)
		{
			throw new ApplicationException($"Status not found: {request.RequestData.Status}");
		}
		 
		if (status.Sort <= order.Status.Sort)
		{
			throw new ApplicationException("Status update is invalid");
		}

		string fromStatus = order.StatusId ?? "";

		order.Status = status;
		order.StatusId = status.Id;
		order.ModifiedDate = DateTime.Now;
		order.ModifiedUser = request.RequestData.ModifiedUser;

		_context.Orders.Update(order);
		int rows = await _context.SaveChangesAsync();

		if(rows > 0)
		{
			await _historyService.CreateHistory(order, fromStatus, status.Name);
			await _eventBus.PublishAsync(new OrderStatusUpdatedEvent()
			{
				Id = order.Id,
				UserId = order.UserId,
				StatusId = status.Id,
				Status = status.Name,
				Point = GetPoint(order, status.Id),
				Products = await GetProduct(order,status.Id)
			});
		}

		return Result<OrderDto>.Success(_mapper.Map<OrderDto>(order));
	}

	private int GetPoint(Order order,string statusId)
	{
		if(statusId == OrderStatusConstant.Canceled)
		{
			return order.PointUsed;
		}

		decimal pointPerTotal = 10;
		return (int)(order.Total / pointPerTotal);
	}

	private async Task<List<ProductCanceled>> GetProduct(Order order, string statusId)
	{
		List<ProductCanceled> data = new List<ProductCanceled>();
		if (statusId != OrderStatusConstant.Canceled)
		{
			return data;
		}
		data = await _context.OrderItems.Where(s => s.OrderId == order.Id)
						     .Select(s => new ProductCanceled()
						     {
							     ProductId = s.ProductId,
							     ProductItemId = s.ProductItemId,
							     VariationId = s.VariationId,
							     Quantity = s.Quantity
						     }).ToListAsync();
		return data;
	}
}
