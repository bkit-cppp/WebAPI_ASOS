namespace BuildingBlock.Messaging.Events;

public class OrderStatusUpdatedEvent
{
	public Guid Id { get; set; }
	public Guid UserId { get; set; }
	public string StatusId { get; set; }
	public string Status { get; set; }
	public int Point { get; set; }
	public List<ProductCanceled> Products { get; set; }
}

public class ProductCanceled
{
	public Guid VariationId { get; set; }
	public Guid ProductItemId { get; set; }
	public Guid ProductId { get; set; }
	public int Quantity { get; set; }
}
