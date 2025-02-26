namespace Catalog.Domain.Entities;

[Table("tb_products")]
public class Product : BaseEntity<Guid>
{
	public Product() : base() { }
	public string Slug { get; set; } = string.Empty;
	public string Name { get; set; } = string.Empty;
	public string Description { get; set; } = string.Empty;
	public decimal AverageRating { get; set; } = 0.0m;
	public Guid? CategoryGenderId { get; set; }
	public Guid? BrandId { get; set; }
    public string? SizeAndFit { get; set; } = string.Empty;
	public string? Image { get; set; } = string.Empty;
	public int? Bought { get; set; } = 0;
	public decimal OriginalPrice { get; set; } = 0;
	public decimal SalePrice { get; set; } = 0;
	public bool IsSale { get; set; } = false;
	[JsonIgnore] public CategoryGender? CategoryGender { get; set; }
	[JsonIgnore] public Brand? Brand { get; set; }
	[JsonIgnore] public virtual ICollection<Comment>? Comments { set; get; }
	[JsonIgnore] public virtual ICollection<WishList>? WishLists { get; set; }
	[JsonIgnore] public virtual ICollection<Rating>? Ratings { get; set; }
	[JsonIgnore] public virtual ICollection<ProductItem>? ProductItems { get; set; }
}
