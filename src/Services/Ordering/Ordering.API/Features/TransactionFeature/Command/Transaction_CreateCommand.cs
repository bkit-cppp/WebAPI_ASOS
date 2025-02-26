using Ordering.API.Features.TransactionFeature.Dtos;

namespace Ordering.API.Features.TransactionFeature.Command;

public record Transaction_CreateCommand(TransactionAddOrUpdate RequestData) : ICommand<Result<TransactionDto>>;
public class Transaction_CreateCommandHandler : ICommandHandler<Transaction_CreateCommand, Result<TransactionDto>>
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;
    public Transaction_CreateCommandHandler(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Result<TransactionDto>> Handle(Transaction_CreateCommand request, CancellationToken cancellationToken)
    {
        var order = await _context.Orders.FindAsync(request.RequestData.OrderId);
        if (order == null)
        {
            throw new ApplicationException($"Order not found: {request.RequestData.OrderId}");
        }
        var refund = await _context.Refunds.FindAsync(request.RequestData.RefundId);
        if (refund == null)
        {
            throw new ApplicationException($"refund not found: {request.RequestData.OrderId}");
        }
        var transaction = new Transaction()
        {
            Refund=refund,
            RefundId=refund.Id,
            BankBranch=request.RequestData.BankBranch,
            BankNumber=request.RequestData.BankNumber,
            Content=request.RequestData.Content,
            Total=request.RequestData.Total,
            Order = order,
            OrderId = order.Id,
            CreatedUser = request.RequestData.CreatedUser,
            ModifiedUser = request.RequestData.CreatedUser
        };

        _context.Transactions.Add(transaction);

        int rows = await _context.SaveChangesAsync();
        if (rows == 0)
        {
            throw new ApplicationException("Failed to create discount.");
        }

        return Result<TransactionDto>.Success(_mapper.Map<TransactionDto>(transaction));
    }
}
