namespace Ordering.API.Features.RefundFeature.Command;

public record Refund_DeleteCommand(DeleteRequest RequestData) : ICommand<Result<bool>>;
public class Refund_DeleteCommandHandler : ICommandHandler<Refund_DeleteCommand, Result<bool>>
{
	private readonly IMapper _mapper;
	private readonly DataContext _dataContext;
	public Refund_DeleteCommandHandler(DataContext dataContext, IMapper mapper)
	{
		_dataContext = dataContext;
		_mapper = mapper;
	}

	public async Task<Result<bool>> Handle(Refund_DeleteCommand request, CancellationToken cancellationToken)
	{
		if (request.RequestData.Ids == null)
			throw new ApplicationException("Ids not found");

		List<Guid> ids = request.RequestData.Ids.Select(m => Guid.Parse(m)).ToList();
		var query = await _dataContext.Refunds.Where(m => ids.Contains(m.Id)).ToListAsync();
		if (query == null || query.Count == 0) throw new ApplicationException($"Không tìm thấy trong dữ liệu có Id: {string.Join(";", request.RequestData.Ids)}");

		foreach (var item in query)
		{
			item.DeleteFlag = true;
			item.ModifiedDate = DateTime.Now;
			item.ModifiedUser = request.RequestData.ModifiedUser;
		}
		_dataContext.Refunds.UpdateRange(query);
		await _dataContext.SaveChangesAsync(cancellationToken);
		return Result<bool>.Success(true);
	}
}
