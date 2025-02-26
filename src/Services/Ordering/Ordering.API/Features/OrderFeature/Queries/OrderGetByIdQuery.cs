using Ordering.API.Features.OrderFeature.Dto;

namespace Ordering.API.Features.OrderFeature.Queries;

public record OrderGetByIdQuery(Guid id) : IQuery<OrderDto>;
public class OrderGetByIdQueryHandler : IQueryHandler<OrderGetByIdQuery, OrderDto>
{
	private readonly DataContext _dataContext;
	public OrderGetByIdQueryHandler(DataContext dataContext)
	{
		_dataContext = dataContext;
	}
	public async Task<OrderDto> Handle(OrderGetByIdQuery request, CancellationToken cancellationToken)
	{
		var order = await _dataContext.Orders.Where(p => p.Id == request.id).Select(order => new OrderDto
		{
			UserId = order.UserId,
			BasePrice = order.BasePrice,
			DiscountId = order.DiscountId,
			DiscountPrice = order.DiscountPrice,
			PointUsed = order.PointUsed,
			StatusId = order.StatusId,
			SubPrice = order.SubPrice,
			Total = order.Total
		}).FirstOrDefaultAsync();

		return order;
	}
}