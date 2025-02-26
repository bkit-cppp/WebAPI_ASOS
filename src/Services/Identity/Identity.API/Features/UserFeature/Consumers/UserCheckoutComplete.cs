using BuildingBlock.Messaging.Events;
using MassTransit;

namespace Identity.API.Features.UserFeature.Consumers;

public sealed class UserCheckoutComplete : IConsumer<OrderCheckoutEvent>
{
	private readonly DataContext _context;
	public UserCheckoutComplete(DataContext context)
	{
		_context = context;
	}

	public async Task Consume(ConsumeContext<OrderCheckoutEvent> consumer)
	{
		var user = await _context.Users.FindAsync(consumer.Message.UserId);

		if (user == null || consumer.Message.PointUsed == 0)
		{
			return;
		}

		var point = new PointHistory()
		{
			UserId = user.Id,
			ModifiedUser = user.Id,
			PointBefore = user.Point,
			PointChange = consumer.Message.PointUsed,
			PointAfter = user.Point - consumer.Message.PointUsed,
			Reason = "Use loyalty points for shopping",
			ReferenceId = consumer.Message.OrderId.ToString(),
			ReferenceType = "Order",
		};

		user.Point = user.Point - consumer.Message.PointUsed;

		_context.Update(user);
		_context.Add(point);

		await _context.SaveChangesAsync();
	}
}
