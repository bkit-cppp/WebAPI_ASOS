namespace Catalog.Domain.Entities;

[Table("tb_sizes")]
public class Size : BaseEntity<Guid>
{
	public Size() : base() { }
	public string Slug { get; set; } = string.Empty;
	public string Name { get; set; } = string.Empty;
	public string? Description { get; set; } = string.Empty;
	[JsonIgnore] public virtual ICollection<SizeCategory>? SizeCategories { get; set; }
	[JsonIgnore] public virtual ICollection<Variation>? Variations { get; set; }
}
