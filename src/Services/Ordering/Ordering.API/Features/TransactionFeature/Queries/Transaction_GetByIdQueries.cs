using Ordering.API.Features.TransactionFeature.Dtos;

namespace Ordering.API.Features.TransactionFeature.Queries;

public record Transaction_GetByIdQueries(Guid Id):IQuery<Result<TransactionDto>>;
public class Transaction_GetByIdQueriesHandler : IQueryHandler<Transaction_GetByIdQueries, Result<TransactionDto>>
{
    private readonly DataContext _dataContext;
    private readonly IMapper _mapper;
    public Transaction_GetByIdQueriesHandler(DataContext dataContext, IMapper mapper)
    {
        _dataContext = dataContext;
        _mapper = mapper;
    }
    public async Task<Result<TransactionDto>> Handle(Transaction_GetByIdQueries request, CancellationToken cancellationToken)
    {
        var transactionDto = await _dataContext.Transactions.Where(s => s.Id == request.Id)
                            .ProjectTo<TransactionDto>(_mapper.ConfigurationProvider)
                            .FirstOrDefaultAsync();
        return Result<TransactionDto>.Success(transactionDto);
    }
}
