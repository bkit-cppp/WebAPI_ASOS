namespace Ordering.API.Features.OrderFeature.Commands;

public record OrderDeleteCommand(DeleteRequest DeleteRequest) : ICommand<Result<bool>>;
public class OrderDeleteCommandHandler : ICommandHandler<OrderDeleteCommand, Result<bool>>
{
	private readonly DataContext _dataContext;
	public OrderDeleteCommandHandler(DataContext dataContext)
	{
		_dataContext = dataContext;
	}
	public async Task<Result<bool>> Handle(OrderDeleteCommand request, CancellationToken cancellationToken)
	{
		if (request.DeleteRequest.Ids == null)
			throw new ApplicationException("Ids not found");

		List<Guid> ids = request.DeleteRequest.Ids.Select(m => Guid.Parse(m)).ToList();
		var query = await _dataContext.Orders.Where(m => ids.Contains(m.Id)).ToListAsync();

		if (query == null || query.Count == 0)
		{
			throw new ApplicationException($"Không tìm thấy trong dữ liệu có Id: {string.Join(";", request.DeleteRequest.Ids)}");
		}

		foreach (var item in query)
		{
			item.DeleteFlag = true;
			item.ModifiedDate = DateTime.Now;
			item.ModifiedUser = request.DeleteRequest.ModifiedUser;
		}

		_dataContext.Orders.UpdateRange(query);
		await _dataContext.SaveChangesAsync(cancellationToken);

		return Result<bool>.Success(true);
	}
}
