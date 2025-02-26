using System.Text.Json.Serialization;

namespace BuildingBlock.Core.Request;

public class DeleteRequest
{
	public List<string> Ids { set; get; }
	[JsonIgnore] public Guid ModifiedUser { get; set; }
	[JsonIgnore] public Guid? RoleId { set; get; }
}
