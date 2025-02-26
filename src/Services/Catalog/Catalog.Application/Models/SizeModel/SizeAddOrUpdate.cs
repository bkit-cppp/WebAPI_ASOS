namespace Catalog.Application.Models.SizeModel;

public class SizeAddOrUpdate : AddOrUpdateRequest
{
	public Guid? Id { get; set; }
	public string Slug { get; set; } = string.Empty;
	public string Name { get; set; } = string.Empty;
	public string? Description { get; set; } = string.Empty;
	public List<Guid>? CategoryIds { get; set; }
}
