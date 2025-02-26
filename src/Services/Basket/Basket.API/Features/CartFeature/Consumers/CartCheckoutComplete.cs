using BuildingBlock.Messaging.Events;
using MassTransit;

namespace Basket.API.Features.CartFeature.Consumers;

public sealed class CartCheckoutComplete : IConsumer<OrderCheckoutEvent>
{
	private readonly ICartService _cartService;
	public CartCheckoutComplete(ICartService cartService)
	{
		_cartService = cartService;
	}
	public async Task Consume(ConsumeContext<OrderCheckoutEvent> consumer)
	{
		await _cartService.ClearCart(consumer.Message.UserId);
	}
}
