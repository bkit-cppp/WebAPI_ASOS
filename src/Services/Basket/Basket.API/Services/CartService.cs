using Basket.API.Features.CartFeature.Dto;
using BuildingBlock.Grpc.Services;

namespace Basket.API.Services;

public class CartService : ICartService
{
	private readonly ICacheService _cacheService;
	private readonly IdentityGrpcService _identityGrpc;
	public CartService(ICacheService cacheService, IdentityGrpcService identityGrpc)
	{
		_cacheService = cacheService;
		_identityGrpc = identityGrpc;
	}

	public async Task<CartDto> GetCart(Guid id)
	{
		var cacheKey = CacheHelper.GetCacheKey(id);
		string cartJson = await _cacheService.GetCacheResponseAsync(cacheKey);
		if (string.IsNullOrEmpty(cartJson))
		{
			var user = await _identityGrpc.GetUserAsync(id);
			CartDto emptyCart = new CartDto(id);
			/*emptyCart.FakeData(Guid.Parse(user.Id));*/
			await SetCache(emptyCart);
			return emptyCart;
		}
		CartDto? cart = JsonConvert.DeserializeObject<CartDto>(cartJson);
		if (cart == null)
		{
			throw new ApplicationException("Data not valid");
		}
		return cart;
	}

	public async Task ClearCart(Guid id)
	{
		var cacheKey = CacheHelper.GetCacheKey(id);
		await _cacheService.RemoveCacheResponseAsync(cacheKey);
	}

	public async Task SetCache(CartDto cart)
	{
		var cacheKey = CacheHelper.GetCacheKey(cart.UserId);
		await _cacheService.SetCacheResponseAsync(cacheKey, cart, CartConstant.TimeOut);
	}
}
