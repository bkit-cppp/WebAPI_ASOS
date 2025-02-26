using Ordering.API.Features.OrderFeature.Dto;

namespace Ordering.API.Features.OrderFeature.Commands;

public record OrderCreateCommand(OrderDto Order) : ICommand<Guid>;
public class OrderCreateCommandHandler : ICommandHandler<OrderCreateCommand, Guid>
{
	private readonly DataContext _dataContext;
	public OrderCreateCommandHandler(DataContext dataContext)
	{
		_dataContext = dataContext;
	}
	public async Task<Guid> Handle(OrderCreateCommand request, CancellationToken cancellationToken)
	{
		var transaction = _dataContext.Transactions.Find(request.Order.TransactionId);
		var orderStatus = _dataContext.OrderStatus.Find(request.Order.StatusId);
		var order = new Order()
		{
			UserId = request.Order.UserId,
			DiscountId = request.Order.DiscountId,
			BasePrice = request.Order.BasePrice,
			DiscountPrice = request.Order.DiscountPrice,
			SubPrice = request.Order.SubPrice,
			PointUsed = request.Order.PointUsed,
			Total = request.Order.Total,
			Status = orderStatus,
			Transaction = transaction

		};
		_dataContext.Orders.Add(order);
		await _dataContext.SaveChangesAsync();
		return order.Id;
	}
}
