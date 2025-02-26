using Basket.API.Features.CartFeature.Dto;
using BuildingBlock.Grpc.Services;
using BuildingBlock.Utilities;

namespace Basket.API.Features.CartFeature.Commands;

public record Cart_ApplyDiscountCommand(DiscountAddOrDeleteRequest requestData) : ICommand<Result<CartDto>>;

public class Cart_ApplyDiscountCommandHandler : ICommandHandler<Cart_ApplyDiscountCommand, Result<CartDto>>
{
	private readonly ICartService _cartService;
	private readonly PromotionGrpcService _promotionGrpc;

	public Cart_ApplyDiscountCommandHandler(ICartService cartService, PromotionGrpcService promotionGrpc)
	{
		_cartService = cartService;
		_promotionGrpc = promotionGrpc;
	}

	public async Task<Result<CartDto>> Handle(Cart_ApplyDiscountCommand request, CancellationToken cancellationToken)
	{
		var discount = await _promotionGrpc.GetDiscountByCode(request.requestData.Code);
		var cart = await _cartService.GetCart(request.requestData.UserId);

		if(discount.DiscountTypeId == "Product")
		{
			if (!cart.Items.Any() || string.IsNullOrEmpty(discount.DiscountProducts))
			{
				throw new ApplicationException("Discount is invalid");
			}
		
			var productIds = ConverterHelper.SplitStringToList<Guid>(discount.DiscountProducts);
			var find = cart.Items.Where(s => productIds.Contains(s.ProductId)).FirstOrDefault();

			if (find == null)
			{
				throw new ApplicationException("Discount is invalid");
			}
		}

		if(cart.BasePrice < (decimal)discount.Minimum)
		{
			var money = (decimal)discount.Minimum - cart.BasePrice;
			throw new ApplicationException($"you are {money} euros short to use this discount code");
		}

		cart.Discount = new DiscountDto()
		{
			Id = Guid.Parse(discount.Id),
			Code = discount.Code,
			Value = (decimal)discount.Value,
			DiscountTypeId = discount.DiscountTypeId
		};

		cart.ProcessData();

		await _cartService.SetCache(cart);

		return Result<CartDto>.Success(cart);
	}
}