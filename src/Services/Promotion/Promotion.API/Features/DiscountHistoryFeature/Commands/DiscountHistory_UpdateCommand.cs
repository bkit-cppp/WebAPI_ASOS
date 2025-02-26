using FluentValidation;
using Promotion.API.Features.DiscountHistoryFeature.Dto;
using Promotion.API.Models;
namespace Promotion.API.Features.DiscountHistoryFeature.Commands;

public record DiscountHistory_UpdateCommand(DiscountHistoryAddOrUpdateRequest RequestData) : ICommand<Result<DiscountHistoryDto>>;

public class DiscountHistoryUpdateCommandValidator : AbstractValidator<DiscountHistory_UpdateCommand>
{
	public DiscountHistoryUpdateCommandValidator()
	{
		RuleFor(command => command.RequestData.DiscountApplied)
			.NotEmpty().WithMessage("Discount applied is required");

		RuleFor(command => command.RequestData.Id)
			.NotEmpty().WithMessage("Discount history ID is required");
	}
}
public class DiscountHistory_UpdateCommandHandler : ICommandHandler<DiscountHistory_UpdateCommand, Result<DiscountHistoryDto>>
{
	private readonly DataContext _context;
	private readonly IMapper _mapper;
	public DiscountHistory_UpdateCommandHandler(DataContext context, IMapper mapper)
	{
		_context = context;
		_mapper = mapper;
	}
	public async Task<Result<DiscountHistoryDto>> Handle(DiscountHistory_UpdateCommand request, CancellationToken cancellationToken)
	{
		var discountHistory = await _context.DiscountHistories.FindAsync(request.RequestData.Id);

		if (discountHistory == null)
		{
			throw new ApplicationException("Discount history not found");
		}

		discountHistory.DiscountApplied = request.RequestData.DiscountApplied;
		discountHistory.ModifiedDate = DateTime.Now;
		discountHistory.ModifiedUser = request.RequestData.ModifiedUser;

		_context.DiscountHistories.Update(discountHistory);
		int rows = await _context.SaveChangesAsync();

		if (rows == 0)
		{
			throw new ApplicationException("Failed to create discount.");
		}

		return Result<DiscountHistoryDto>.Success(_mapper.Map<DiscountHistoryDto>(discountHistory));
	}
}
