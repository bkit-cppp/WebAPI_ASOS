namespace Ordering.API.Features.OrderItemFeature.Command;

public record OrderItemDeleteCommand(DeleteRequest RequestData) : ICommand<Result<bool>>;
public class OrderItem_DeleteCommandHandler : ICommandHandler<OrderItemDeleteCommand, Result<bool>>
{
	private readonly DataContext _dataContext;
	private readonly IMapper _mapper;
	public OrderItem_DeleteCommandHandler(IMapper mapper, DataContext dataContext)
	{
		_mapper = mapper;
		_dataContext = dataContext;
	}
	public async Task<Result<bool>> Handle(OrderItemDeleteCommand request, CancellationToken cancellationToken)
	{
		if (request.RequestData.Ids == null)
			throw new ApplicationException("Ids not found");

		List<Guid> ids = request.RequestData.Ids.Select(m => Guid.Parse(m)).ToList();
		var query = await _dataContext.OrderItems.Where(m => ids.Contains(m.Id)).ToListAsync();
		if (query == null || query.Count == 0) throw new ApplicationException($"Không tìm thấy trong dữ liệu có Id: {string.Join(";", request.RequestData.Ids)}");

		foreach (var item in query)
		{
			item.DeleteFlag = true;
			item.ModifiedDate = DateTime.Now;
			item.ModifiedUser = request.RequestData.ModifiedUser;
		}
		_dataContext.OrderItems.UpdateRange(query);
		await _dataContext.SaveChangesAsync(cancellationToken);
		return Result<bool>.Success(true);
	}
}
