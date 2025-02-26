namespace Catalog.Domain.Entities;

[Table("tb_comments")]
public class Comment : BaseEntity<Guid>
{
	public Comment() : base() { }
	public string Content { get; set; } = string.Empty;
	public Guid? ParentId { get; set; }
	public Guid? UserId { get; set; }
    [JsonIgnore] public UserComment? User { get; set; }
	public Guid? ProductId { get; set; }
	[JsonIgnore] public Product? Product { get; set; }
}
