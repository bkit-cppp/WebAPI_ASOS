using System.Text.Json.Serialization;

namespace BuildingBlock.Core.Request;

public class UpdateStatusRequest
{
	public Guid Id { get; set; }
	public string Status { get; set; }
	[JsonIgnore] public Guid? ModifiedUser { set; get; }
	[JsonIgnore] public string? RoleId { set; get; }
}
