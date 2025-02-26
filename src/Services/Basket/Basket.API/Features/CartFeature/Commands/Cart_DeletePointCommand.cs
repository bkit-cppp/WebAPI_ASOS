using Basket.API.Features.CartFeature.Dto;

namespace Basket.API.Features.CartFeature.Commands;

public record Cart_DeletePointCommand(Guid user) : ICommand<Result<CartDto>>;

public class Cart_DeletePointCommandHandler : ICommandHandler<Cart_DeletePointCommand, Result<CartDto>>
{
	private readonly ICartService _cartService;

	public Cart_DeletePointCommandHandler(ICartService cartService)
	{
		_cartService = cartService;
	}

	public async Task<Result<CartDto>> Handle(Cart_DeletePointCommand request, CancellationToken cancellationToken)
	{
		var cart = await _cartService.GetCart(request.user);

		cart.PointUsed = 0;
		cart.ProcessData();

		await _cartService.SetCache(cart);

		return Result<CartDto>.Success(cart);
	}
}