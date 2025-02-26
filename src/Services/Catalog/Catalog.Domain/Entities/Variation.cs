namespace Catalog.Domain.Entities;

[Table("tb_variations")]
public class Variation : BaseEntity<Guid>
{
	public Variation() : base() { }
	public Guid? ProductItemId { get; set; }
	public Guid? SizeId { get; set; }
	public int QtyDisplay { get; set; } = 0;
	public int QtyInStock { get; set; } = 0;
	public decimal Stock { get; set; } = 0;
	[JsonIgnore] public ProductItem? ProductItem { get; set; }
	[JsonIgnore] public Size? Size { get; set; }
}
