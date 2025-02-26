using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Identity.API.Data;

[Table("tb_hub_connections")]
public class HubConnection
{
	public HubConnection()
	{
		Id = Guid.NewGuid();
		ConnectedTime = DateTime.Now;
		ExpireTime = DateTime.Now.AddDays(1);
	}

	[Key]
	public Guid Id { get; set; }
	public Guid? UserId { get; set; }
	public User? User { get; set; }
	public string ConnectionId { get; set; } = string.Empty;
	public string Device { get; set; } = "PC";
	public string IPAddress { get; set; } = string.Empty;
	public DateTime ConnectedTime { get; set; } = DateTime.Now;
	public DateTime ExpireTime { get; set; } = DateTime.Now.AddDays(1);
}
