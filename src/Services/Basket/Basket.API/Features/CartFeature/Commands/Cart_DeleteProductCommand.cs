using Basket.API.Features.CartFeature.Dto;

namespace Basket.API.Features.CartFeature.Commands;

public record Cart_DeleteProductCommand(CartAddOrDeleteProductRequest RequestData) : ICommand<Result<CartDto>>;

public class CartDeleteCommandValidator : AbstractValidator<Cart_DeleteProductCommand>
{
	public CartDeleteCommandValidator()
	{
		RuleFor(command => command.RequestData.UserId).NotEmpty().WithMessage("User not found");

		RuleFor(command => command.RequestData.VariationId).NotEmpty().WithMessage("Product not found");
	}
}

public class Cart_DeleteProductCommandHandler : ICommandHandler<Cart_DeleteProductCommand, Result<CartDto>>
{
	private readonly ICartService _cartService;

	public Cart_DeleteProductCommandHandler(ICartService cartService)
	{
		_cartService = cartService;
	}

	public async Task<Result<CartDto>> Handle(Cart_DeleteProductCommand request, CancellationToken cancellationToken)
	{
		var cart = await _cartService.GetCart(request.RequestData.UserId);

		if (!cart.Items.Any())
		{
			throw new ApplicationException("Cart is empty cannot delete product");
		}

		var variation = cart.Items.Where(s => s.VariationId == request.RequestData.VariationId)
							.FirstOrDefault();

		if(variation == null)
		{
			throw new ApplicationException("Product not found");
		}

		cart.Items.Remove(variation);
		cart.ProcessData();

		await _cartService.SetCache(cart);

		return Result<CartDto>.Success(cart);
	}
}