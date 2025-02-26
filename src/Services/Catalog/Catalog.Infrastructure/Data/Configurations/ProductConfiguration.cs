using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.Infrastructure.Data.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
	public void Configure(EntityTypeBuilder<Product> builder)
	{
		builder.HasQueryFilter(p => p.DeleteFlag != true);
        builder.HasOne(x => x.CategoryGender).WithMany(x => x.Products).HasForeignKey(x => x.CategoryGenderId);
        builder.HasOne(x => x.Brand).WithMany(x => x.Products).HasForeignKey(x => x.BrandId);

    }
}
