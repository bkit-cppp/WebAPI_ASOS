namespace Ordering.API.Interfaces;

public interface IOrderHistoryService
{
	Task<OrderHistory> CreateHistory(Order order, string fromStatus,string toStatus);
}
