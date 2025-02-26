using Ordering.API.Interfaces;

namespace Ordering.API.Implements;

public class OrderHistoryService : IOrderHistoryService
{
	private readonly DataContext _context;
	public OrderHistoryService(DataContext context)
	{
		_context = context;
	}

	public async Task<OrderHistory> CreateHistory(Order order, string fromStatus, string toStatus)
	{
		var history = new OrderHistory()
		{
			OrderId = order.Id,
			FromStatus = fromStatus,
			ToStatus = toStatus,
			CreatedUser = order.UserId,
		};

		var result = await _context.OrderHistories.AddAsync(history);
		await _context.SaveChangesAsync();
		
		return result.Entity;
	}
}
