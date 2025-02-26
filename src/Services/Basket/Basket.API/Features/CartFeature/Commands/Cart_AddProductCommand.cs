using Basket.API.Features.CartFeature.Dto;
using BuildingBlock.Grpc.Services;

namespace Basket.API.Features.CartFeature.Commands;

public record Cart_AddProductCommand(CartAddOrDeleteProductRequest RequestData) : ICommand<Result<CartDto>>;

public class CartAddCommandValidator : AbstractValidator<Cart_AddProductCommand>
{
	public CartAddCommandValidator()
	{

		RuleFor(command => command.RequestData.UserId).NotEmpty().WithMessage("User not found");

		RuleFor(command => command.RequestData.VariationId).NotEmpty().WithMessage("Product not found");
	}
}

public class Cart_AddProductCommandHandler : ICommandHandler<Cart_AddProductCommand, Result<CartDto>>
{
	private readonly ICartService _cartService;
	private readonly CatalogGrpcService _catalogService;

	public Cart_AddProductCommandHandler(ICartService cartService, CatalogGrpcService catalogService)
	{
		_cartService = cartService;
		_catalogService = catalogService;
	}

	public async Task<Result<CartDto>> Handle(Cart_AddProductCommand request, CancellationToken cancellationToken)
	{
		var cart = await _cartService.GetCart(request.RequestData.UserId);

		var variation = cart.Items.Where(s => s.VariationId == request.RequestData.VariationId)
							.FirstOrDefault();

		if (!cart.Items.Any() || variation == null)
		{
			var product = await _catalogService.GetProductAsync(request.RequestData.VariationId);
			var cartItem = new CartItemDto
			{
				ProductId = Guid.Parse(product.ProductId),
				ProductItemId = Guid.Parse(product.ProductItemId),
				VariationId = Guid.Parse(product.VariationId),
				Slug = product.Slug,
				Name = product.Name,
				Description = "",
				Category = product.Category,
				Brand = product.Brand,
				Size = product.Size,
				Color = product.Color,
				OriginalPrice = (decimal)product.OriginalPrice,
				SalePrice = (decimal)product.SalePrice,
				AdditionalPrice = (decimal)product.AdditionalPrice,
				Stock = (decimal)product.Stock,
				IsSale = product.IsSale,
				Quantity = 1,
				Image = product.Image
			};
			cart.Items.Add(cartItem);
		}
		else
		{
			variation!.Quantity++;
		}

		cart.ProcessData();
		await _cartService.SetCache(cart);

		return Result<CartDto>.Success(cart);
	}
}