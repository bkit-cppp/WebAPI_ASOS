namespace Catalog.Application.Features.GenderFeature.Dto;

public class GenderMenuDto
{
	public Guid Id { get; set; }
	public string Slug { get; set; }
	public string Name { get; set; }
	public List<CategoryInGender> Categories { get; set; }
}

public class CategoryInGender
{
	public Guid Id { get; set; }
	public string Slug { get; set; }
	public string Name { get; set; }
	public Guid? ParentId { get; set; }
	public List<CategoryInGender> Children { get; set; } = new List<CategoryInGender>();
}