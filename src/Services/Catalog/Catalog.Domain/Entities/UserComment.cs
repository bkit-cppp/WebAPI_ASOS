namespace Catalog.Domain.Entities;

[Table("tb_user_comments")]
public class UserComment : BaseEntity<Guid>
{
	public UserComment() : base() { }
	public string Email { get; set; } = string.Empty;
	public string Fullname { get; set; } = string.Empty;
	public string Avatar { get; set; } = string.Empty;
	[JsonIgnore] public ICollection<Comment>? Comments { set; get; }
}
