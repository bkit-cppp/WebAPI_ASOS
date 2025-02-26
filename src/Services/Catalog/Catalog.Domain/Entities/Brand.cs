namespace Catalog.Domain.Entities;

[Table("tb_brands")]
public class Brand : BaseEntity<Guid>
{
	public Brand() : base() { }
	public string Slug { get; set; } = string.Empty;
	public string Name { get; set; } = string.Empty;
	public string Description { get; set; } = string.Empty;
	public string Image { get; set; } = string.Empty;
	[JsonIgnore] public virtual ICollection<Product>? Products { get; set; }
}
