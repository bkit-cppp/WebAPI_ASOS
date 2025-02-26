using BuildingBlock.Core.Domain;
using System.ComponentModel.DataAnnotations.Schema;

namespace Identity.API.Data;

[Table("tb_point_histories")]
public class PointHistory : BaseEntity<Guid>
{
	public string ReferenceId { get; set; } = string.Empty;
	public string ReferenceType { get; set; } = string.Empty;
	public int PointBefore { get; set; } = 0;
	public int PointChange { get; set; } = 0;
	public int PointAfter { get; set; } = 0;
	public string Reason { get; set; } = string.Empty;	
	public Guid? UserId { get; set; }
	public User? User { get; set; }
}
