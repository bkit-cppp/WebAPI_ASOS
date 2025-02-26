namespace Catalog.Domain.Entities;

[Table("tb_category_gender")]
public class CategoryGender : BaseEntity<Guid>
{
	public Guid CategoryId { get; set; }
	public Guid GenderId { get; set; }
	public Category Category { get; set; }
	public Gender Gender { get; set; }
	[JsonIgnore] public virtual ICollection<Product>? Products { set; get; }
}
