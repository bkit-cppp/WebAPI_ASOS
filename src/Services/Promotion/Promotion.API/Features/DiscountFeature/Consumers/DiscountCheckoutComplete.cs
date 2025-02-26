using BuildingBlock.Messaging.Events;
using MassTransit;
using Microsoft.Data.SqlClient;

namespace Promotion.API.Features.DiscountFeature.Consumers;

public sealed class DiscountCheckoutComplete : IConsumer<OrderCheckoutEvent>
{
	private readonly DataContext _context;
	public DiscountCheckoutComplete(DataContext context)
	{
		_context = context;
	}

	public async Task Consume(ConsumeContext<OrderCheckoutEvent> consumer)
	{
		if (consumer.Message.DiscountId == null)
		{
			return;
		}

		/*var discount = await _context.Discounts.FindAsync(consumer.Message.DiscountId);
		if (discount == null)
		{
			return;
		}

		discount.Quantity = discount.Quantity - 1;
		_context.Update(discount);

		await _context.SaveChangesAsync();*/

		var discountIdParam = new SqlParameter("@DiscountId", consumer.Message.DiscountId);

		await _context.Database.ExecuteSqlRawAsync(
			"UPDATE Discounts SET Quantity = Quantity - 1 WHERE Id = @DiscountId AND Quantity > 0",
			discountIdParam);
	}
}
