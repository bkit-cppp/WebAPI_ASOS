using Basket.API.Features.CartFeature.Dto;

namespace Basket.API.Features.CartFeature.Commands;

public record Cart_DeleteDiscountCommand(DiscountAddOrDeleteRequest requestData) : ICommand<Result<CartDto>>;

public class Cart_DeleteDiscountCommandHandler : ICommandHandler<Cart_DeleteDiscountCommand, Result<CartDto>>
{
	private readonly ICartService _cartService;

	public Cart_DeleteDiscountCommandHandler(ICartService cartService)
	{
		_cartService = cartService;
	}

	public async Task<Result<CartDto>> Handle(Cart_DeleteDiscountCommand request, CancellationToken cancellationToken)
	{
		var cart = await _cartService.GetCart(request.requestData.UserId);

		cart.Discount = null;
		cart.ProcessData();

		await _cartService.SetCache(cart);

		return Result<CartDto>.Success(cart);
	}
}