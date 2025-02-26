using System.ComponentModel.DataAnnotations.Schema;

namespace Ordering.API.Data;

[Table("tb_order_items")]
public class OrderItem : BaseEntity<Guid>
{
	public OrderItem() : base() { }
	public Guid OrderId { get; set; }	
	public Order Order { get; set; }
	public Guid ProductId { get; set; }
	public Guid ProductItemId { get; set; }
	public Guid VariationId { get; set; }
	public string Name { get; set; } = string.Empty;
	public string Brand { get; set; } = string.Empty;
	public string Category { get; set; } = string.Empty;
	public string Size { get; set; } = string.Empty;
	public string Color { get; set; } = string.Empty;
	public string Image { get; set; } = string.Empty;
	public decimal Price { get; set; } = 0;
	public decimal Stock { get; set; } = 0;
	public int Quantity { get; set; } = 0;
	public decimal Amount { get; set; } = 0;
}
