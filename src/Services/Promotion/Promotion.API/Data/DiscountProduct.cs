using BuildingBlock.Core.Domain;
using System.ComponentModel.DataAnnotations.Schema;

namespace Promotion.API.Data;

[Table("tb_discount_products")]
public class DiscountProduct : BaseEntity<Guid>
{
	public DiscountProduct() : base() 
	{ 
		Id = Guid.NewGuid();
	}
	public Discount? Discount { get; set; }
    public Guid? DiscountId { get; set; }
    public Guid? ProductId { get; set; }

}
