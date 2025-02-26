using System.ComponentModel.DataAnnotations.Schema;

namespace Ordering.API.Data;

[Table("tb_transactions")]
public class Transaction : BaseEntity<Guid>
{
	public Transaction() : base() { }
	public Guid OrderId { get; set; }
	public Order Order { get; set; }
	public Guid? RefundId { get; set; }
	public Refund? Refund { get; set; }
	public decimal Total { get; set; }
	public string Content { get; set; }
	public string BankBranch { get; set; }
	public string BankNumber { get; set; }
}
