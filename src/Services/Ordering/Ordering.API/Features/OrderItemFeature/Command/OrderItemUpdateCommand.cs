using Ordering.API.Features.OrderItemFeature.Dto;

namespace Ordering.API.Features.OrderItemFeature.Command;

public record OrderItemUpdateCommand(OrderItemAddOrUpdate RequestData) : ICommand<Result<OrderItemDto>>;
public class OrderItem_UpdateCommandHandler : ICommandHandler<OrderItemUpdateCommand, Result<OrderItemDto>>
{
	private readonly DataContext _dataContext;
	private readonly IMapper _mapper;
	public OrderItem_UpdateCommandHandler(DataContext dataContext, IMapper mapper)
	{
		_dataContext = dataContext;
		_mapper = mapper;
	}
	public async Task<Result<OrderItemDto>> Handle(OrderItemUpdateCommand request, CancellationToken cancellationToken)
	{
		var orderItem = await _dataContext.OrderItems.FindAsync(request.RequestData.Id);
		if (orderItem == null)
		{
			return Result<OrderItemDto>.Failure("Không có dữ liệu");
		}
		orderItem.Amount = request.RequestData.Amount;
		orderItem.Price = request.RequestData.Price;
		orderItem.Quantity = request.RequestData.Quantity;
		orderItem.Size = request.RequestData.Size;
		orderItem.Name = request.RequestData.Name;
		orderItem.Category = request.RequestData.Category;
		orderItem.Color = request.RequestData.Color;
		orderItem.Stock = request.RequestData.Stock;
		orderItem.Brand = request.RequestData.Brand;
		orderItem.Image = request.RequestData.Image;
		_dataContext.OrderItems.Update(orderItem);
		await _dataContext.SaveChangesAsync();
		return Result<OrderItemDto>.Success(_mapper.Map<OrderItemDto>(orderItem));
	}
}

