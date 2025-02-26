using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.Infrastructure.Data.Configurations
{
    public class ProductItemConfiguration : IEntityTypeConfiguration<ProductItem>
    {
        public void Configure(EntityTypeBuilder<ProductItem> builder)
        {
			builder.HasQueryFilter(p => p.DeleteFlag != true);
			builder.HasOne(x => x.Color).WithMany(x => x.ProductItems).HasForeignKey(x => x.ColorId);
            builder.HasOne(x => x.Product).WithMany(x => x.ProductItems).HasForeignKey(x => x.ProductId);
        }
    }
}
