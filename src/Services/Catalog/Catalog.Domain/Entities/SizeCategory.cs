namespace Catalog.Domain.Entities;

[Table("tb_size_category")]
public class SizeCategory:BaseEntity<Guid>
{
	public SizeCategory() : base() { }
    public Guid? CategoryId { get; set; }
    public Guid? SizeId { get; set; }
	[JsonIgnore] public Category? Category { get; set; }
	[JsonIgnore] public Size? Size{ get; set; }
}
