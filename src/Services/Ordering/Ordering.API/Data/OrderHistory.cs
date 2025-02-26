using System.ComponentModel.DataAnnotations.Schema;

namespace Ordering.API.Data;

[Table("tb_order_histories")]
public class OrderHistory : BaseEntity<Guid>
{
	public OrderHistory() : base() { }
	public Guid OrderId { get; set; }
	public Order Order { get; set; }
	public string ToStatus { get; set; }
	public string FromStatus { get; set; }
}
