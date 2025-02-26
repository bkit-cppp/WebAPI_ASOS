namespace Catalog.Domain.Entities;

[Table("tb_genders")]
public class Gender : BaseEntity<Guid>
{
	public Gender() : base() { }
	public string Slug { get; set; } = string.Empty;
	public string Name { get; set; } = string.Empty;
	public string? Description { get; set; } = string.Empty;
	[JsonIgnore] public virtual ICollection<CategoryGender>? CategoryGenders { get; set; }
}
