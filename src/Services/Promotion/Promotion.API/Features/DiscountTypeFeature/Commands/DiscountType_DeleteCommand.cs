namespace Promotion.API.Features.DiscountTypeFeature.Commands;

public record DiscountType_DeleteCommand(DeleteRequest RequestData) : ICommand<Result<bool>>;
public class DiscountType_DeleteCommandHandler : ICommandHandler<DiscountType_DeleteCommand, Result<bool>>
{

	private readonly DataContext _context;

	public DiscountType_DeleteCommandHandler(IMapper mapper, DataContext context)
	{
		_context = context;
	}
	public async Task<Result<bool>> Handle(DiscountType_DeleteCommand request, CancellationToken cancellationToken)
	{
		if (request.RequestData?.Ids == null || !request.RequestData.Ids.Any())
			throw new ApplicationException("Ids are required");

		var ids = request.RequestData.Ids;
		var discountTypes = await _context.DiscountTypes
								  .Where(m => ids.Contains(m.Id))
								  .ToListAsync(cancellationToken);

		foreach (var item in discountTypes)
		{
			item.DeleteFlag = true;
			item.ModifiedDate = DateTime.Now;
			item.ModifiedUser = request.RequestData.ModifiedUser;
		}

		_context.DiscountTypes.UpdateRange(discountTypes);

		await _context.SaveChangesAsync(cancellationToken);

		return Result<bool>.Success(true);
	}
}
