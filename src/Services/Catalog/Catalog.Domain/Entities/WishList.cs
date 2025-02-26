namespace Catalog.Domain.Entities;

[Table("tb_wishlists")]
public class WishList : BaseEntity<Guid>
{
	public WishList() : base() { }
	public Guid? ProductId { get; set; }
	public Guid? UserId { get; set; }
	[JsonIgnore] public Product? Product { get; set; }
}
