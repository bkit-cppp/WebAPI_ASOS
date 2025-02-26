using Basket.API.Features.CartFeature.Dto;

namespace Basket.API.Interfaces;

public interface ICartService
{
	Task<CartDto> GetCart(Guid id);
	Task SetCache(CartDto cart);
	Task ClearCart(Guid id);
}
