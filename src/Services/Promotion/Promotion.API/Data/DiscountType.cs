using BuildingBlock.Core.Domain;
using System.ComponentModel.DataAnnotations.Schema;

namespace Promotion.API.Data;

[Table("tb_discount_types")]
public class DiscountType : BaseEntity<string>
{
	public DiscountType() : base()
	{
		Id = "";
	}
	public string Name { get; set; } = string.Empty;
	public string? Description { get; set; } = string.Empty;
	public List<Discount>? Discounts { get; set; }
}
