namespace Catalog.Domain.Entities;

[Table("tb_colors")]
public class Color : BaseEntity<Guid>
{
	public Color() : base() { }
	public string Slug { get; set; } = string.Empty;
	public string Name { get; set; } = string.Empty;
	public string? Description { get; set; } = string.Empty;
	[JsonIgnore] public virtual ICollection<ProductItem>? ProductItems { get; set; }
}
