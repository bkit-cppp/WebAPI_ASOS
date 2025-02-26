using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.Infrastructure.Data.Configurations
{
    public class ImageConfiguration : IEntityTypeConfiguration<Image>
    {
        public void Configure(EntityTypeBuilder<Image> builder)
        {
			builder.HasQueryFilter(p => p.DeleteFlag != true);
			builder.HasOne(x => x.ProductItem).WithMany(x => x.Images).HasForeignKey(x => x.ProductItemId);
        }
    }
}
