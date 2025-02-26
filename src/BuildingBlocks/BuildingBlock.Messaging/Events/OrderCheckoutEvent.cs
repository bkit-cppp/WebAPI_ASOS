namespace BuildingBlock.Messaging.Events;

public class OrderCheckoutEvent
{
	public Guid OrderId { get; set; }
	public Guid UserId { get; set; }
	public Guid? DiscountId { get; set; }
	public int PointUsed { get; set; }
	public List<VariationCheckout> Variations { get; set; }
}

public class VariationCheckout
{
	public Guid VariationId { get; set; }
	public int Quantity { get; set; }
}
