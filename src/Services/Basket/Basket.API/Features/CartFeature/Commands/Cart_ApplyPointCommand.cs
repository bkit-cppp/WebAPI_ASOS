using Basket.API.Features.CartFeature.Dto;
namespace Basket.API.Features.CartFeature.Commands;

public record Cart_ApplyPointCommand(Guid user,int point) : ICommand<Result<CartDto>>;

public class Cart_ApplyPointCommandHandler : ICommandHandler<Cart_ApplyPointCommand, Result<CartDto>>
{
    private readonly ICartService _cartService;

    public Cart_ApplyPointCommandHandler(ICartService cartService)
    {
        _cartService = cartService;
    }

    public async Task<Result<CartDto>> Handle(Cart_ApplyPointCommand request, CancellationToken cancellationToken)
    {
        var cart = await _cartService.GetCart(request.user);

        cart.PointUsed = request.point;

        cart.ProcessData();

        await _cartService.SetCache(cart);

        return Result<CartDto>.Success(cart);
    }
}