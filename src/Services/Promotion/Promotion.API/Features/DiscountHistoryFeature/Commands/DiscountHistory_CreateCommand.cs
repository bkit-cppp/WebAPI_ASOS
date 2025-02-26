using FluentValidation;
using Promotion.API.Features.DiscountHistoryFeature.Dto;
using Promotion.API.Models;

namespace Promotion.API.Features.DiscountHistoryFeature.Commands;

public record DiscountHistory_CreateCommand(DiscountHistoryAddOrUpdateRequest RequestData) : ICommand<Result<DiscountHistoryDto>>;
public class DiscountHistoryCreateCommandValidator : AbstractValidator<DiscountHistory_CreateCommand>
{
	public DiscountHistoryCreateCommandValidator()
	{
		RuleFor(command => command.RequestData.DiscountApplied)
			.NotEmpty().WithMessage("Discount applied is required");

		RuleFor(command => command.RequestData.DiscountId)
			.NotEmpty().WithMessage("Discount ID is required");
	}
}
public class DiscountHistory_CreateCommandHandler : ICommandHandler<DiscountHistory_CreateCommand, Result<DiscountHistoryDto>>
{
	private readonly DataContext _context;
	private readonly IMapper _mapper;
	public DiscountHistory_CreateCommandHandler(DataContext context, IMapper mapper)
	{
		_context = context;
		_mapper = mapper;
	}
	public async Task<Result<DiscountHistoryDto>> Handle(DiscountHistory_CreateCommand request, CancellationToken cancellationToken)
    {
		var discount = await _context.Discounts.FindAsync(request.RequestData.DiscountId);
		if (discount == null)
		{
			throw new ApplicationException($"Discount not found: {request.RequestData.DiscountId}");
		}

		var discountHistory = new DiscountHistory()
        {
            DiscountApplied = request.RequestData.DiscountApplied,
            Discount = discount,
            DiscountId = discount.Id,
			CreatedUser = request.RequestData.CreatedUser,
			ModifiedUser = request.RequestData.CreatedUser
		};
  
        _context.DiscountHistories.Add(discountHistory);

        int rows = await _context.SaveChangesAsync();
        if (rows == 0)
        {
            throw new ApplicationException("Failed to create discount.");
        }

		return Result<DiscountHistoryDto>.Success(_mapper.Map<DiscountHistoryDto>(discountHistory));
	}
}
