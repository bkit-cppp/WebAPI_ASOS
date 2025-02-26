namespace Ordering.API.Features.RefundFeature.Command;

public record Refund_CreateCommand(RefundAddOrUpdateRequest RequestData) : ICommand<Guid>;
public class Refund_CreateCommandHandler : ICommandHandler<Refund_CreateCommand, Guid>
{
	private readonly IMapper _mapper;
	private readonly DataContext _dataContext;
	public Refund_CreateCommandHandler(IMapper mapper, DataContext dataContext)
	{
		_mapper = mapper;
		_dataContext = dataContext;
	}
	public async Task<Guid> Handle(Refund_CreateCommand request, CancellationToken cancellationToken)
	{
		var transaction = _dataContext.Transactions.Find(request.RequestData.TransactionId);

		var refund = new Refund()
		{
			RefundAmount = request.RequestData.RefundAmount,
			Reason = request.RequestData.Reason,
			Transaction = transaction

		};
		_dataContext.Refunds.Add(refund);
		await _dataContext.SaveChangesAsync();
		return refund.Id;
	}
}



