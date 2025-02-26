using BuildingBlock.Grpc.Protos;
using Grpc.Core;

namespace Basket.API.Services;

public class BasketService : BasketGrpc.BasketGrpcBase
{
	private readonly ICartService _cartService;
	public BasketService(ICartService cartService)
	{
		_cartService = cartService;
	}

	public override async Task<GetCartTotalResponse> GetTotal(GetCartTotalRequest request, ServerCallContext context)
	{
		var cart = await _cartService.GetCart(Guid.Parse(request.User));
		if (cart == null || cart.Items.Count == 0)
		{
			throw new ApplicationException("Cart is empty, cannot check out");
		}
		return new GetCartTotalResponse()
		{
			Success = true,
			ErrMessage = "",
			Total = (double)cart.Total
		};
	}

	public override async Task<GetCartResponse> GetCart(GetCartRequest request, ServerCallContext context)
	{
		var cart = await _cartService.GetCart(Guid.Parse(request.User));
		if (cart == null || cart.Items.Count == 0)
		{
			throw new ApplicationException("Cart is empty, cannot check out");
		}
		var response = new GetCartResponse()
		{
			Success = true,
			ErrMessage = "",
			UserId = cart.UserId.ToString(),
			DiscountId = cart.Discount != null ? cart.Discount.Id.ToString() : "",
			BasePrice = (double)cart.BasePrice,
			DiscountPrice = (double)cart.DiscountPrice,
			PointUsed = cart.PointUsed,
			SubPrice = (double)cart.SubPrice,
			Total = (double)cart.Total
		};
		response.Items.AddRange(cart.Items.Select(item => new CartItem
		{
			ProductId = item.ProductId.ToString(),
			ProductItemId = item.ProductItemId.ToString(),
			Quantity = item.Quantity,
			Slug = item.Slug,
			Name = item.Name,
			Description = item.Description,
			Category = item.Category,
			Brand = item.Brand,
			Size = item.Size,
			Color = item.Color,
			OriginalPrice = (double)item.OriginalPrice,
			SalePrice = (double)item.SalePrice,
			AdditionalPrice = (double)item.AdditionalPrice,
			Stock = (double)item.Stock,
			IsSale = item.IsSale,
			VariationId = item.VariationId.ToString(),
			Image = item.Image,
			FinalPrice = (double)item.FinalTotal,
		}));
		return response;
	}
}
