using Basket.API.Features.CartFeature.Dto;

namespace Basket.API.Features.CartFeature.Commands;

public record Cart_UpdateQuantityCommand(CartUpdateQuantityRequest RequestData) : ICommand<Result<CartDto>>;

public class CartUpdateQuantityCommandValidator : AbstractValidator<Cart_UpdateQuantityCommand>
{
	public CartUpdateQuantityCommandValidator()
	{

		RuleFor(command => command.RequestData.UserId).NotEmpty().WithMessage("User not found");

		RuleFor(command => command.RequestData.VariationId).NotEmpty().WithMessage("Product not found");
	}
}

public class Cart_UpdateQuantityCommandHandler : ICommandHandler<Cart_UpdateQuantityCommand, Result<CartDto>>
{
	private readonly ICartService _cartService;

	public Cart_UpdateQuantityCommandHandler(ICartService cartService)
	{
		_cartService = cartService;
	}

	public async Task<Result<CartDto>> Handle(Cart_UpdateQuantityCommand request, CancellationToken cancellationToken)
	{
		var cart = await _cartService.GetCart(request.RequestData.UserId);

		var variation = cart.Items.Where(s => s.VariationId == request.RequestData.VariationId)
							.FirstOrDefault();

		if(variation == null)
		{
			throw new ApplicationException($"Product {request.RequestData.VariationId} not found in cart");
		}

		if(request.RequestData.Update == Enums.CartUpdate.Increase)
		{
			variation.Quantity++;
		}
		if (request.RequestData.Update == Enums.CartUpdate.Decrease)
		{
			variation.Quantity--;
			if(variation.Quantity <= 0)
			{
				cart.Items.Remove(variation);
			}
		}

		cart.ProcessData();
		await _cartService.SetCache(cart);

		return Result<CartDto>.Success(cart);
	}
}