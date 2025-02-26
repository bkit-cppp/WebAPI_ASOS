namespace Catalog.Domain.Entities;

[Table("tb_product_items")]
public class ProductItem : BaseEntity<Guid>
{
	public ProductItem() : base() { }
	public Guid? ProductId { get; set; }
	public Guid? ColorId { get; set; }
	public decimal AdditionalPrice { get; set; }

	/*	public decimal OriginalPrice { get; set; }
		public decimal SalePrice { get; set; }
		public bool IsSale { get; set; }  */

	[JsonIgnore] public Color? Color { get; set; }
	[JsonIgnore] public Product? Product { get; set; }
	[JsonIgnore] public virtual ICollection<Variation>? Variations { get; set; }
	[JsonIgnore] public virtual ICollection<Image>? Images { get; set; }
}
