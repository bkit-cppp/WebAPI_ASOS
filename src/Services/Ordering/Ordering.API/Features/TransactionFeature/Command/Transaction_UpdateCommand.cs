using Ordering.API.Features.TransactionFeature.Dtos;

namespace Ordering.API.Features.TransactionFeature.Command;

public record Transaction_UpdateCommand(TransactionAddOrUpdate RequestData) : ICommand<Result<TransactionDto>>;
public class Transaction_UpdateCommandHandler : ICommandHandler<Transaction_UpdateCommand, Result<TransactionDto>>
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;
    public Transaction_UpdateCommandHandler(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<Result<TransactionDto>> Handle(Transaction_UpdateCommand request, CancellationToken cancellationToken)
    {
        var transaction = await _context.Transactions.FindAsync(request.RequestData.Id);

        if (transaction == null)
        {
            throw new ApplicationException("Order history not found");
        }

        transaction.Content = request.RequestData.Content;
        transaction.BankBranch = request.RequestData.BankBranch;
        transaction.BankNumber = request.RequestData.BankNumber;
        transaction.Total = request.RequestData.Total;
        transaction.ModifiedDate = DateTime.Now;
        transaction.ModifiedUser = request.RequestData.ModifiedUser;

        _context.Transactions.Update(transaction);
        int rows = await _context.SaveChangesAsync();

        if (rows == 0)
        {
            throw new ApplicationException("Failed to create discount.");
        }

        return Result<TransactionDto>.Success(_mapper.Map<TransactionDto>(transaction));
    }
}
