using Ordering.API.Features.TransactionFeature.Dtos;


namespace Ordering.API.Features.TransactionFeature.Queries;

public record Transaction_GetpaginationQueries(PaginationRequest RequestData) : IQuery<Result<PaginatedList<TransactionDto>>>;
public class Transaction_GetpaginationQueriesHandler : IQueryHandler<Transaction_GetpaginationQueries, Result<PaginatedList<TransactionDto>>>
{
	private readonly IMapper _mapper;
	private readonly DataContext _dataContext;
	public Transaction_GetpaginationQueriesHandler(IMapper mapper, DataContext dataContext)
	{
		_mapper = mapper;
		_dataContext = dataContext;
	}
	public async Task<Result<PaginatedList<TransactionDto>>> Handle(Transaction_GetpaginationQueries request, CancellationToken cancellationToken)
	{
		var orderCol = request.RequestData.OrderCol;
		var orderDir = request.RequestData.OrderDir;

		var query = _dataContext.Transactions.OrderedListQuery(orderCol, orderDir)
							.ProjectTo<TransactionDto>(_mapper.ConfigurationProvider)
							.AsNoTracking();

		var paging = await query.PaginatedListAsync(request.RequestData.PageIndex, request.RequestData.PageSize);
		return Result<PaginatedList<TransactionDto>>.Success(paging);
	}
}
