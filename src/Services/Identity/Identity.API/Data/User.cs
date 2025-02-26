using BuildingBlock.Core.Domain;
using System.ComponentModel.DataAnnotations.Schema;
namespace Identity.API.Data;

[Table("tb_users")]
public class User : BaseEntity<Guid>
{
	public User() : base() 
	{
		Id = Guid.NewGuid();
		Avatar = AvatarConstant.Default;
		IsEmailConfirmed = false;
	}
	public string Email { get; set; } = string.Empty;
	public string Password { get; set; } = string.Empty;
	public string Phone { get; set; } = string.Empty;
	public string Fullname { get; set; } = string.Empty;
	public string Address { get; set; } = string.Empty;
	public bool IsEmailConfirmed { get; set; } = false;
	public string? Avatar { get; set; } = string.Empty;
	public int Point { get; set; } = 0;
	public string? BanReason { get; set; } = string.Empty;
	public string? RoleId { get; set; }
	public Role? Role { get; set; }
	public string? StatusId { get; set; }
	public Status? Status { get; set; }
	public ICollection<Token>? Tokens { set; get; }
	public ICollection<HubConnection>? HubConnections { set; get; }
	public ICollection<PointHistory>? PointHistories { set; get; }
	public ICollection<OTP>? OTPs { set; get; }
	public ICollection<Notification>? Notifications { set; get; }
}
