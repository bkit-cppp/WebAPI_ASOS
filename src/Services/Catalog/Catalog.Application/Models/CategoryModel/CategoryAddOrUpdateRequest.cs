namespace Catalog.Application.Models.CategoryModel;

public class CategoryAddOrUpdateRequest : AddOrUpdateRequest
{
	public Guid? Id { get; set; }
	public string Slug { get; set; } = string.Empty;
	public string Name { get; set; } = string.Empty;
	public string? Description { get; set; } = string.Empty;
	public string? ImageFile { get; set; } = string.Empty;
	public Guid? ParentId { get; set; }
	public List<Guid>? Genders { get; set; } = new List<Guid>();
}
