using Ordering.API.Features.OrderStatusFeature.Dto;

namespace Ordering.API.Features.OrderStatusFeature.Commands
{
	public record OrderStatus_UpdateCommand(OrderStatusAddOrUpdate RequestData) : ICommand<Result<OrderStatusDto>>;
	public class OrderStatus_UpdateCommandHandler : ICommandHandler<OrderStatus_UpdateCommand, Result<OrderStatusDto>>
	{
		private readonly DataContext _dataContext;
		private readonly IMapper _mapper;
		public OrderStatus_UpdateCommandHandler(DataContext dataContext, IMapper mapper)
		{
			_dataContext = dataContext;
			_mapper = mapper;
		}
		public async Task<Result<OrderStatusDto>> Handle(OrderStatus_UpdateCommand request, CancellationToken cancellationToken)
		{
			var orderStatus = await _dataContext.OrderStatus.FindAsync(request.RequestData.Id);
			if (orderStatus == null)
			{
				return Result<OrderStatusDto>.Failure("Không có dữ liệu");
			}
			orderStatus.Description = request.RequestData.Description;
			orderStatus.Name = request.RequestData.Name;
			_dataContext.OrderStatus.Update(orderStatus);
			await _dataContext.SaveChangesAsync();
			return Result<OrderStatusDto>.Success(_mapper.Map<OrderStatusDto>(orderStatus));
		}
	}

}
