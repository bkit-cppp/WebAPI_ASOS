using Ordering.API.Features.TransactionFeature.Dtos;

namespace Ordering.API.Features.TransactionFeature.Queries;

public record Transaction_GetAllQueries(BaseRequest RequestData) : IQuery<Result<IEnumerable<TransactionDto>>>;
public class Transaction_GetAllQueriesHandler : IQueryHandler<Transaction_GetAllQueries, Result<IEnumerable<TransactionDto>>>
{
	private readonly DataContext _dataContext;
	private readonly IMapper _mapper;

	public Transaction_GetAllQueriesHandler(IMapper mapper, DataContext dataContext)
	{
		_dataContext = dataContext;
		_mapper = mapper;
	}

	public async Task<Result<IEnumerable<TransactionDto>>> Handle(Transaction_GetAllQueries request, CancellationToken cancellationToken)
	{
		var orderCol = request.RequestData.OrderCol;
		var orderDir = request.RequestData.OrderDir;

		IEnumerable<TransactionDto> tranSaction = await _dataContext.Transactions.OrderedListQuery(orderCol, orderDir)
											   .ProjectTo<TransactionDto>(_mapper.ConfigurationProvider)
											   .ToListAsync();

		return Result<IEnumerable<TransactionDto>>.Success(tranSaction);
	}
}
