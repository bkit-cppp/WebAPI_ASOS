using FluentValidation;
using Promotion.API.Features.DiscountProductFeature.Dto;
using Promotion.API.Models;

namespace Promotion.API.Features.DiscountProductFeature.Commands;

public record DiscountProduct_UpdateCommand(DiscountProductAddOrUpdateRequest RequestData) : ICommand<Result<List<DiscountProductDto>>>;

public class DiscountProductUpdateCommandValidator : AbstractValidator<DiscountProduct_UpdateCommand>
{
	public DiscountProductUpdateCommandValidator()
	{
		RuleFor(command => command.RequestData.DiscountId).NotEmpty().WithMessage("Discount ID is required");

		RuleFor(command => command.RequestData.ProductIds).NotEmpty().WithMessage("Product IDs is required");
	}
}
public class DiscountProduct_UpdateCommandHandler : ICommandHandler<DiscountProduct_UpdateCommand, Result<List<DiscountProductDto>>>
{
	private readonly DataContext _context;
	private readonly IMapper _mapper;
	public DiscountProduct_UpdateCommandHandler(DataContext context, IMapper mapper)
	{
		_context = context;
		_mapper = mapper;
	}
	public async Task<Result<List<DiscountProductDto>>> Handle(DiscountProduct_UpdateCommand request, CancellationToken cancellationToken)
    {
		var dtos = new List<DiscountProductDto>();

		var discount = await _context.Discounts.Include(s => s.DiscountProducts)
                       .FirstOrDefaultAsync(s => s.Id == request.RequestData.DiscountId);

		if (discount == null)
		{
			throw new ApplicationException("Discount not found");
		}

		if (discount.DiscountTypeId != DiscountTypeConstant.Product)
		{
			throw new ApplicationException("Discount type invalid");
		}

		if (discount.DiscountProducts != null && discount.DiscountProducts.Any())
        {
            _context.DiscountProducts.RemoveRange(discount.DiscountProducts);
		}

		List<DiscountProduct> discountProducts = new List<DiscountProduct>();
		foreach (var productId in request.RequestData.ProductIds)
		{
			DiscountProduct discountProduct = new DiscountProduct()
			{
				Discount = discount,
				DiscountId = discount.Id,
				ProductId = productId,
				CreatedUser = request.RequestData.CreatedUser,
				ModifiedUser = request.RequestData.CreatedUser
			};
			discountProducts.Add(discountProduct);
			dtos.Add(_mapper.Map<DiscountProductDto>(discountProduct));
		}

		_context.DiscountProducts.AddRange(discountProducts);
		int rows = await _context.SaveChangesAsync();
        if (rows == 0)
        {
			throw new ApplicationException("Failed to create discount.");
        }

		return Result<List<DiscountProductDto>>.Success(dtos);
	}
}
