namespace Ordering.API.Features.TransactionFeature.Command;

public record Transaction_DeleteCommand(DeleteRequest RequestData) : ICommand<Result<bool>>;
public class Transaction_DeleteCommandHandler : ICommandHandler<Transaction_DeleteCommand, Result<bool>>
{
	private readonly IMapper _mapper;
	private readonly DataContext _dataContext;
	public Transaction_DeleteCommandHandler(DataContext dataContext, IMapper mapper)
	{
		_dataContext = dataContext;
		_mapper = mapper;
	}
	public async Task<Result<bool>> Handle(Transaction_DeleteCommand request, CancellationToken cancellationToken)
	{
		if (request.RequestData.Ids == null)
			throw new ApplicationException("Ids not found");

		List<Guid> ids = request.RequestData.Ids.Select(m => Guid.Parse(m)).ToList();
		var query = await _dataContext.Transactions.Where(m => ids.Contains(m.Id)).ToListAsync();
		if (query == null || query.Count == 0) throw new ApplicationException($"Không tìm thấy trong dữ liệu có Id: {string.Join(";", request.RequestData.Ids)}");

		foreach (var item in query)
		{
			item.DeleteFlag = true;
			item.ModifiedDate = DateTime.Now;
			item.ModifiedUser = request.RequestData.ModifiedUser;
		}
		_dataContext.Transactions.UpdateRange(query);
		await _dataContext.SaveChangesAsync(cancellationToken);
		return Result<bool>.Success(true);
	}
}

