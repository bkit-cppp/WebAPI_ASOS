using BuildingBlock.Core.Domain;
using System.ComponentModel.DataAnnotations.Schema;

namespace Identity.API.Data;

[Table("tb_tokens")]
public class Token : BaseEntity<Guid>
{
	public Guid? UserId { get; set; }
	public User? User { get; set; }
	public string AccessToken { get; set; } = string.Empty;
	public string RefreshToken { get; set; } = string.Empty;
	public DateTime ExpireAt { get; set; }
	public string Device { get; set; } = "PC";
	public string? IPAddress { get; set; } = string.Empty;
	public bool IsExpired { get; set; } = false;
}
