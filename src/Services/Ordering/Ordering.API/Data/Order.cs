using System.ComponentModel.DataAnnotations.Schema;

namespace Ordering.API.Data;

[Table("tb_orders")]
public class Order : BaseEntity<Guid>
{
	public Order() : base() {}
	public Guid UserId { get; set; }
	public Guid? DiscountId { get; set; }
	public decimal BasePrice { get; set; } = 0;
	public decimal DiscountPrice { get; set; } = 0;
	public decimal SubPrice { get; set; } = 0;
	public int PointUsed { get; set; } = 0;
	public decimal Total { get; set; } = 0;
	public string? StatusId { get; set; }
	public string ReceiverName { get; set; }
	public string Email { get; set; }
	public string Phone { get; set; }
	public string Address { get; set; }
	public OrderStatus? Status { get; set; }
	public Guid? TransactionId { get; set; }
	public Transaction? Transaction { get; set; }
	public ICollection<OrderItem>? OrderItems { set; get; }
	public ICollection<OrderHistory>? OrderHistories { set; get; }
}
