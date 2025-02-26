using Basket.API.Features.CartFeature.Dto;

namespace Basket.API.Features.CartFeature.Queries;

public record Cart_GetByUserQuery(Guid user) : IQuery<Result<CartDto>>;
public class Cart_GetByUserQueryHandler : IQueryHandler<Cart_GetByUserQuery, Result<CartDto>>
{
	private readonly ICartService _cartService;

	public Cart_GetByUserQueryHandler(ICartService cartService)
	{
		_cartService = cartService;
	}

	public async Task<Result<CartDto>> Handle(Cart_GetByUserQuery request, CancellationToken cancellationToken)
	{
		if(request.user == Guid.Empty)
		{
			throw new ApplicationException("User not found");
		}
		CartDto cart = await _cartService.GetCart(request.user);
		return Result<CartDto>.Success(cart);
	}
}