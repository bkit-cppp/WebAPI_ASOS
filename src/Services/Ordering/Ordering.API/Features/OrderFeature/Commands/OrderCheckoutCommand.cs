using BuildingBlock.Grpc.Protos;
using BuildingBlock.Messaging.Abstractions;
using BuildingBlock.Messaging.Events;
using Ordering.API.Features.OrderFeature.Dto;

namespace Ordering.API.Features.OrderFeature.Commands;

public record OrderCheckoutCommand(Guid id, PaymentCallbackModel param) : ICommand<Result<OrderDto>>;
public class OrderCheckoutCommandHandler : ICommandHandler<OrderCheckoutCommand, Result<OrderDto>>
{
	private readonly DataContext _context;
	private readonly BasketGrpcService _basketService;
	private readonly IOrderHistoryService _historyService;
	private readonly IMapper _mapper;
	private readonly IEventBus _eventBus;
	public OrderCheckoutCommandHandler(
		DataContext context, 
		BasketGrpcService basketService,
		IOrderHistoryService historyService,
		IEventBus eventBus,
		IMapper mapper)
	{
		_context = context;
		_basketService = basketService;
		_historyService = historyService;
		_eventBus = eventBus;
		_mapper = mapper;
	}
	public async Task<Result<OrderDto>> Handle(OrderCheckoutCommand request, CancellationToken cancellationToken)
	{
		var order = await _context.Orders.FindAsync(request.id);

		if (order == null)
		{
			throw new ApplicationException("Order not found");
		}
		if (order.StatusId != OrderStatusConstant.Pending)
		{
			throw new ApplicationException("Order status is invalid");
		}

		var cart = await _basketService.GetCartAsync(order.UserId);

		if (order.Total != (decimal)cart.Total)
		{
			throw new ApplicationException("Order total is invalid");
		}

		var status = await _context.OrderStatus.FindAsync(OrderStatusConstant.Placed);

		if(status == null)
		{
			throw new ApplicationException("Status not found");
		}

		string fromStatus = order.StatusId ?? "";

		if (!string.IsNullOrEmpty(cart.DiscountId))
		{
			order.DiscountId = Guid.Parse(cart.DiscountId);
		}
		order.DiscountPrice = (decimal)cart.DiscountPrice;
		order.BasePrice = (decimal)cart.BasePrice;
		order.SubPrice = (decimal)cart.SubPrice;
		order.PointUsed = cart.PointUsed;
		order.StatusId = status.Id;
		order.Status = status;
		order.ModifiedDate = DateTime.Now;
		order.TransactionId = CreateTransaction(order, request.param);

		var variations = new List<VariationCheckout>();
		foreach (var item in cart.Items)
		{
			CreateOrderItem(order, item);

			VariationCheckout variation = new VariationCheckout()
			{
				VariationId = Guid.Parse(item.VariationId),
				Quantity = item.Quantity
			};
			variations.Add(variation);
		}

		_context.Orders.Update(order);

		int rows = await _context.SaveChangesAsync();
		if (rows > 0)
		{
			await _historyService.CreateHistory(order, fromStatus, status.Name);
			await PublishEvent(order, variations);
		}

		return Result<OrderDto>.Success(_mapper.Map<OrderDto>(order));
	}

	private Guid CreateTransaction(Order order, PaymentCallbackModel param)
	{
		var trasaction = new Transaction()
		{
			OrderId = order.Id,
			CreatedUser = order.CreatedUser,
			Content = "Order checkout",
			BankNumber = param.Vnp_BankCode,
			BankBranch = param.Vnp_BankTranNo,
			Total = param.Vnp_Amount,
			ModifiedUser = order.ModifiedUser,
		};
		_context.Transactions.Add(trasaction);
		return trasaction.Id;
	}

	private void CreateOrderItem(Order order, CartItem item)
	{
		var price = item.IsSale == true ? item.SalePrice : item.OriginalPrice;
		OrderItem orderItem = new OrderItem()
		{
			OrderId = order.Id,
			ProductId = Guid.Parse(item.ProductId),
			ProductItemId = Guid.Parse(item.ProductItemId),
			VariationId = Guid.Parse(item.VariationId),
			Name = item.Name,
			Brand = item.Brand,
			Category = item.Category,
			Size = item.Size,
			Color = item.Color,
			Quantity = item.Quantity,
			Image = item.Image,
			Price = (decimal)price,
			Stock = (decimal)item.Stock,
			Amount = (decimal)item.FinalPrice,
			CreatedDate = order.CreatedDate,
			ModifiedDate = order.ModifiedDate,
			CreatedUser = order.CreatedUser,
			ModifiedUser = order.ModifiedUser,
		};
		_context.OrderItems.Add(orderItem);
	}

	private async Task PublishEvent(Order order, List<VariationCheckout> variations)
	{
		await _eventBus.PublishAsync(new OrderCheckoutEvent()
		{
			OrderId = order.Id,
			DiscountId = order.DiscountId,
			PointUsed = order.PointUsed,
			UserId = order.UserId,
			Variations = variations
		});
	}
}