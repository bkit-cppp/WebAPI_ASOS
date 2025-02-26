using BuildingBlock.Core.Domain;
using System.ComponentModel.DataAnnotations.Schema;

namespace Identity.API.Data;

[Table("tb_statuses")]
public class Status : BaseEntity<string>
{
	public Status() : base() {}
	public string Name { get; set; } = string.Empty;
	public string? Description { get; set; } = string.Empty;
	public ICollection<User>? Users { set; get; }
}