namespace Ordering.API.Data.Seeding;

public class DataContextInitializer : IDataContextInitializer
{
	private readonly DataContext _context;
	public DataContextInitializer(DataContext context)
	{
		_context = context;
	}
    public async Task<int> InitOrderStatus()
    {
        int rows = 0;
        if (!_context.OrderStatus.Any())
        {
            var statuses = new List<OrderStatus>
            {
                new OrderStatus
                {
                    Id = OrderStatusConstant.Pending,
                    Name = "Pending Payment",
                    Description = "Pending",
                    Sort = 1
                },
                new OrderStatus
                {
                    Id = OrderStatusConstant.Placed,
                    Name = "Order Placed",
                    Description = "Placed",
                    Sort = 2,
                },
                new OrderStatus
                {
                    Id = OrderStatusConstant.Packed,
                    Name = "Order Packaged",
                    Description = "Packed",
                    Sort = 3,
                },
                new OrderStatus
                {
                    Id = OrderStatusConstant.Shipping,
                    Name = "Shipping",
                    Description = "Shipping",
                    Sort = 4,
                },
				new OrderStatus
                {
                    Id = OrderStatusConstant.Completed,
                    Name = "Completed",
                    Description = "Completed",
                    Sort = 5,
				},
				new OrderStatus
				{
					Id = OrderStatusConstant.Canceled,
					Name = "Canceled",
					Description = "Canceled",
					Sort = 5,
				}
			};
            _context.OrderStatus.AddRange(statuses);
            rows = await _context.SaveChangesAsync();
        }
        return rows;
    }

    public async Task<int> InitOrder()
    {
        int rows = 0;
        if (!_context.Orders.Any())
        {
            /*var orders = new List<Order>
            {
                new Order
                {
                    Id = Guid.NewGuid(),
                    Total = 500000,
                    BasePrice = 450000,
                    PointUsed = 100,
                    DiscountId = Guid.NewGuid(),
                    DiscountPrice = 50000,
                    SubPrice = 400000,
                    StatusId = OrderStatusConstant.Placed, // Sử dụng giá trị hợp lệ từ OrderStatusConstant
                    CreatedDate = DateTime.UtcNow,
                    CreatedUser = Guid.NewGuid()
                },
                new Order
                {
                    Id = Guid.NewGuid(),
                    Total = 600000,
                    BasePrice = 550000,
                    PointUsed = 200,
                    DiscountId = Guid.NewGuid(),
                    DiscountPrice = 50000,
                    SubPrice = 500000,
                    StatusId = OrderStatusConstant.Shipping, // Sử dụng giá trị hợp lệ từ OrderStatusConstant
                    CreatedDate = DateTime.UtcNow,
                    CreatedUser = Guid.NewGuid()
                }
            };
            _context.Orders.AddRange(orders);
            rows = await _context.SaveChangesAsync();*/
        }
        return rows;
    }

    public async Task SeedAsync()
    {
        try
        {
            var statusRows = await InitOrderStatus();
            var orderRows = await InitOrder();
            Console.WriteLine($"Seeded {statusRows} OrderStatus records and {orderRows} Order records.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while seeding data: {ex.Message}");
            throw;
        }
    }
}
