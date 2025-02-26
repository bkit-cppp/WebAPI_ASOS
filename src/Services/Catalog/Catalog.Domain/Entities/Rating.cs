namespace Catalog.Domain.Entities;

[Table("tb_ratings")]
public class Rating:BaseEntity<Guid>
{
	public Rating() : base() { }
    public int Rate { get; set; }
    public Guid? UserId { get; set; }
	public Guid? ProductId { get; set; }
	[JsonIgnore] public Product? Product { get; set; }
}
