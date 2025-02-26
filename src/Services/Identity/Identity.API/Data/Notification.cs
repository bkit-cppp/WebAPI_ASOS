using BuildingBlock.Core.Domain;
using System.ComponentModel.DataAnnotations.Schema;

namespace Identity.API.Data;

[Table("tb_notifications")]
public class Notification : BaseEntity<Guid>
{
	public Notification() : base() { }
	public Guid? UserId { get; set; }
	public User? User { get; set; }
	public string Title { get; set; } = string.Empty;
	public string Content { get; set; } = string.Empty;
	public string? Navigate { get; set; } = string.Empty;
	public NotificationVariant? Variant { get; set; }
}

public enum NotificationVariant
{
	Success,
	Error,
	Info
}