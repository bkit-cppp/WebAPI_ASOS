using Ordering.API.Features.TransactionFeature.Dtos;

namespace Ordering.API.Features.TransactionFeature.Queries;

public record Transaction_GetFilterQueries(OrderHistoryFilterRequest RequestData) : IQuery<Result<IEnumerable<TransactionDto>>>;
public class Transaction_GetFilterQueriesHandler : IQueryHandler<Transaction_GetFilterQueries, Result<IEnumerable<TransactionDto>>>
{
    private readonly IMapper _mapper;
    private readonly DataContext _dataContext;
    public Transaction_GetFilterQueriesHandler(IMapper mapper, DataContext dataContext)
    {
        _dataContext = dataContext;
        _mapper = mapper;

    }
    public async Task<Result<IEnumerable<TransactionDto>>> Handle(Transaction_GetFilterQueries request, CancellationToken cancellationToken)
    {
        var orderCol = request.RequestData.OrderCol;
        var orderDir = request.RequestData.OrderDir;

        var query = _dataContext.OrderHistories.OrderedListQuery(orderCol, orderDir)
                            .ProjectTo<TransactionDto>(_mapper.ConfigurationProvider)
                            .AsNoTracking();

        if (!StringHelper.GuidIsNull(request.RequestData.OrderId))
        {
            query = query.Where(s => s.OrderId == request.RequestData.OrderId);
        }

        if (!string.IsNullOrEmpty(request.RequestData.TextSearch))
        {
            query = query.Where(s => s.BankBranch == request.RequestData.TextSearch);
        }
        if (!string.IsNullOrEmpty(request.RequestData.TextSearch))
        {
            query = query.Where(s => s.BankNumber == request.RequestData.TextSearch);
        }
        if (!string.IsNullOrEmpty(request.RequestData.TextSearch))
        {
            query = query.Where(s => s.Content == request.RequestData.TextSearch);
        }
        if (!string.IsNullOrEmpty(request.RequestData.TextSearch))
        {
            query = query.Where(s => s.Total == decimal.Parse(request.RequestData.TextSearch));
        }
        if (request.RequestData.Skip != null)
        {
            query = query.Skip(request.RequestData.Skip.Value);
        }

        if (request.RequestData.TotalRecord != null)
        {
            query = query.Take(request.RequestData.TotalRecord.Value);
        }

        return Result<IEnumerable<TransactionDto>>.Success(await query.ToListAsync());
    }
}

