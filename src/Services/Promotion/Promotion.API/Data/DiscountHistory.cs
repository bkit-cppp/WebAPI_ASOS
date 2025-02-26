using BuildingBlock.Core.Domain;
using System.ComponentModel.DataAnnotations.Schema;

namespace Promotion.API.Data;

[Table("tb_discount_histories")]
public class DiscountHistory : BaseEntity<Guid>
{
    public DiscountHistory() : base()
    {
        Id = Guid.NewGuid();
    }
    public decimal DiscountApplied { get; set; }
    public Guid? DiscountId { get; set; }
    public Discount? Discount { get; set; }
}
