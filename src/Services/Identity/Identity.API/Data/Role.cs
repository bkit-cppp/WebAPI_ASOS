using BuildingBlock.Core.Domain;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Identity.API.Data;

[Table("tb_roles")]
public class Role : BaseEntity<string>
{
	public Role() : base() {}
	public string Name { get; set; } = string.Empty;
	public string? Description { get; set; } = string.Empty;
	[JsonIgnore] public ICollection<User>? Users { set; get; }
}

