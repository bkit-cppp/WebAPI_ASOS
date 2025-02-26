using BuildingBlock.Core.Domain;
using System.ComponentModel.DataAnnotations.Schema;

namespace Promotion.API.Data;

// có BaseEntity thì ko cần khai báo thêm primary key
[Table("tb_discounts")]
public class Discount : BaseEntity<Guid>
{
	public Discount() : base()
	{
        Id = Guid.NewGuid();
	}
	public DateTime StartDate {get; set; }
    public DateTime EndDate { get; set; }
    public string Code { get; set; } = string.Empty;
    public string Condition { get; set; } = string.Empty;
    public int Quantity { get; set; }
	public bool Available { get; set; } = false;
	public decimal Value { get; set; } = 0;
    public decimal Minimum { get; set; } = 0;
    public string? DiscountTypeId { get; set; }
	public DiscountType? DiscountType { get; set; }
	public List<DiscountHistory>? DiscountHistories { get; set; }
	public List<DiscountProduct>? DiscountProducts {  get; set; }
}
