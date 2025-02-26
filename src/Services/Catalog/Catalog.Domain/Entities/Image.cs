namespace Catalog.Domain.Entities;

[Table("tb_images")]
public class Image : BaseEntity<Guid>
{
	public Image() : base() { }
	public string Url { get; set; } = string.Empty;
	public Guid? ProductItemId { get; set; }
	[JsonIgnore] public ProductItem? ProductItem { get; set; }
}
