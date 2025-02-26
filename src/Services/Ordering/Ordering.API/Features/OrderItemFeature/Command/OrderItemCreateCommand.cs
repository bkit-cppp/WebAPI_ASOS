namespace Ordering.API.Features.OrderItemFeature.Command;

public record OrderItemCreateCommand(OrderItem OrderItem) : ICommand<Guid>;
public class OrderItemCreateCommandHandler : ICommandHandler<OrderItemCreateCommand, Guid>
{
	private readonly IMapper _mapper;
	private readonly DataContext _dataContext;
	public OrderItemCreateCommandHandler(DataContext dataContext, IMapper mapper)
	{
		_dataContext = dataContext;
		_mapper = mapper;
	}
	public async Task<Guid> Handle(OrderItemCreateCommand request, CancellationToken cancellationToken)
	{
		var orderItem = new OrderItem()
		{
			Amount = request.OrderItem.Amount,
			Category = request.OrderItem.Category,
			Color = request.OrderItem.Color,
			Brand = request.OrderItem.Brand,
			Name = request.OrderItem.Name,
			ModifiedDate = DateTime.Now,
			CreatedDate = DateTime.Now,
			Quantity = request.OrderItem.Quantity,
			Size = request.OrderItem.Size,
			Price = request.OrderItem.Price,
			Stock = request.OrderItem.Stock,
			Image = request.OrderItem.Image,
			ProductId = Guid.NewGuid(),
			OrderId = Guid.NewGuid()
		};

		_dataContext.OrderItems.Add(orderItem);
		await _dataContext.SaveChangesAsync();

		return orderItem.Id;

	}
}
