using Promotion.API.Features.DiscountFeature.Dto;
using Promotion.API.Models;
using FluentValidation;

namespace Promotion.API.Features.DiscountFeature.Commands;

public record Discount_CreateCommand(DiscountAddOrUpdateRequest RequestData) : ICommand<Result<DiscountDto>>;

public class DiscountCreateCommandValidator : AbstractValidator<Discount_CreateCommand>
{
	public DiscountCreateCommandValidator()
	{
		RuleFor(command => command.RequestData.StartDate).NotEmpty().WithMessage("Start date is required");

		RuleFor(command => command.RequestData.EndDate).NotEmpty().WithMessage("End date is required");

		RuleFor(command => command.RequestData.Code).NotEmpty().WithMessage("Code is required");

		RuleFor(command => command.RequestData.Minimum).NotEmpty().WithMessage("Minimun is required");

		RuleFor(command => command.RequestData.Condition).NotEmpty().WithMessage("Condition is required");

		RuleFor(command => command.RequestData.Quantity).NotEmpty().WithMessage("Quantity is required");

		//RuleFor(command => command.RequestData.Available).NotEmpty().WithMessage("Available is required");

		RuleFor(command => command.RequestData.DiscountTypeId).NotEmpty().WithMessage("Discount Type is required");
	}
}
public class Discount_CreateCommandHandler : ICommandHandler<Discount_CreateCommand, Result<DiscountDto>>
{
	private readonly DataContext _context;
	private readonly IMapper _mapper;
	public Discount_CreateCommandHandler(DataContext context, IMapper mapper)
	{
		_context = context;
		_mapper = mapper;
	}
	public async Task<Result<DiscountDto>> Handle(Discount_CreateCommand request, CancellationToken cancellationToken)
    {
        var discount = new Discount();
        discount.StartDate = request.RequestData.StartDate;
        discount.EndDate = request.RequestData.EndDate;
        discount.Code = request.RequestData.Code;
        discount.Value = request.RequestData.Value;
        discount.Minimum = request.RequestData.Minimum;
		discount.Condition = request.RequestData.Condition;
		discount.Quantity = request.RequestData.Quantity;
		discount.Available = request.RequestData.Available;

		if (discount.EndDate < discount.StartDate)
		{
			throw new ApplicationException("EndDate is invalid");
		}

		if (discount.Value < 0)
		{
			throw new ApplicationException("Value is invalid");
		}

		if (discount.Minimum < 0)
		{
			throw new ApplicationException("Minimum is invalid");
		}

		var checkCode = await _context.Discounts.FirstOrDefaultAsync(s => s.Code ==  discount.Code);
		if(checkCode != null)
		{
			throw new ApplicationException("Discount code already in uses");
		}

		var type = await _context.DiscountTypes.FindAsync(request.RequestData.DiscountTypeId);
		if (type is null)
		{
			throw new ApplicationException($"Discount type not found: {request.RequestData.DiscountTypeId}");
		}
		discount.DiscountType = type;
		discount.DiscountTypeId = type.Id;

        _context.Discounts.Add(discount);
        int rows = await _context.SaveChangesAsync();
        if (rows == 0)
        {
            throw new ApplicationException("Failed to create discount.");
        }

		return Result<DiscountDto>.Success(_mapper.Map<DiscountDto>(discount));
	}
}
