using System.ComponentModel.DataAnnotations.Schema;

namespace Ordering.API.Data;

[Table("tb_refunds")]
public class Refund : BaseEntity<Guid>
{
	public Refund() : base() { }
	public Guid? TransactionId { get; set; }
	public Transaction Transaction { get; set; }
	public decimal RefundAmount { get; set; }
	public string Reason { get; set; }
}
