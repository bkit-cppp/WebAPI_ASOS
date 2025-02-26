using BuildingBlock.Core.Request;

namespace Ordering.API.Models;

public class OrderStatusAddOrUpdate:AddOrUpdateRequest
{
	public string? Id { get; set; }
	public string Name { get; set; } = string.Empty;
	public string? Description { get; set; } = string.Empty;
}
