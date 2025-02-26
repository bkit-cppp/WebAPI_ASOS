namespace Catalog.Application.Models.GenderModel;

public class GenderAddOrUpdate : AddOrUpdateRequest
{
	public string Slug { get; set; } = string.Empty;
	public string Name { get; set; } = string.Empty;
	public string? Description { get; set; } = string.Empty;
}
