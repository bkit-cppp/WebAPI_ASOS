using Ordering.API.Features.RefundFeature.Dtos;

namespace Ordering.API.Features.RefundFeature.Command;

public record Refund_UpdateCommand(RefundAddOrUpdateRequest RequestData) : ICommand<Result<RefundDto>>;
public class Refund_UpdateCommandHandler : ICommandHandler<Refund_UpdateCommand, Result<RefundDto>>
{
	private readonly IMapper _mapper;
	private readonly DataContext _dataContext;
	public Refund_UpdateCommandHandler(IMapper mapper, DataContext dataContext)
	{
		_mapper = mapper;
		_dataContext = dataContext;
	}
	public async Task<Result<RefundDto>> Handle(Refund_UpdateCommand request, CancellationToken cancellationToken)
	{
		var refund = await _dataContext.Refunds.FindAsync(request.RequestData.Id);
		if (refund == null)
		{
			throw new ApplicationException("Refunds not found");
		}
		refund.Reason = request.RequestData.Reason;
		refund.RefundAmount = request.RequestData.RefundAmount;
		refund.ModifiedDate = DateTime.Now;
		refund.ModifiedUser = request.RequestData.ModifiedUser;
		_dataContext.Refunds.Update(refund);
		await _dataContext.SaveChangesAsync();
		return Result<RefundDto>.Success(_mapper.Map<RefundDto>(refund));
	}
}
