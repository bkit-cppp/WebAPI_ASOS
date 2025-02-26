using Basket.API.Features.CartFeature.Dto;

namespace Basket.API.Features.CartFeature.Commands;

public record Cart_ClearCommand(Guid user) : ICommand<Result<CartDto>>;

public class Cart_ClearCommandHandler : ICommandHandler<Cart_ClearCommand, Result<CartDto>>
{
	private readonly ICartService _cartService;

	public Cart_ClearCommandHandler(ICartService cartService)
	{
		_cartService = cartService;
	}

	public async Task<Result<CartDto>> Handle(Cart_ClearCommand request, CancellationToken cancellationToken)
	{
		var cart = await _cartService.GetCart(request.user);

		cart.Discount = null;
		cart.Items = new List<CartItemDto>();
		cart.PointUsed = 0;
		cart.ProcessData();

		await _cartService.SetCache(cart);

		return Result<CartDto>.Success(cart);
	}
}