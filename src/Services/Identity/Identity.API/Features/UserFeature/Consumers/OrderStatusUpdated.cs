using BuildingBlock.Messaging.Events;
using MassTransit;

namespace Identity.API.Features.UserFeature.Consumers;

public class OrderStatusUpdated : IConsumer<OrderStatusUpdatedEvent>
{
	private readonly DataContext _context;
	public OrderStatusUpdated(DataContext context)
	{
		_context = context;
	}

	public async Task Consume(ConsumeContext<OrderStatusUpdatedEvent> consumer)
	{
		if (consumer.Message.StatusId != OrderStatusConstant.Completed && consumer.Message.StatusId != OrderStatusConstant.Canceled)
		{
			return;
		}
		
		if (consumer.Message.Point == 0)
		{
			return;
		}

		var user = await _context.Users.FindAsync(consumer.Message.UserId);

		if (user == null)
		{
			return;
		}

		var history = new PointHistory()
		{
			UserId = user.Id,
			ModifiedUser = user.Id,
			PointBefore = user.Point,
			PointChange = consumer.Message.Point,
			PointAfter = user.Point + consumer.Message.Point,
			Reason = "Use loyalty points for shopping",
			ReferenceId = consumer.Message.Id.ToString(),
			ReferenceType = "Order",
		};

		user.Point = user.Point + consumer.Message.Point;

		if (consumer.Message.StatusId == OrderStatusConstant.Canceled)
		{
			history.Reason = "Points are refunded due to the cancellation of the order";
		}

		if (consumer.Message.StatusId == OrderStatusConstant.Completed)
		{
			history.Reason = "Points are received due to the order was completed";
		}

		_context.PointHistories.Add(history);
		_context.Users.Update(user);

		await _context.SaveChangesAsync();
	}
}
