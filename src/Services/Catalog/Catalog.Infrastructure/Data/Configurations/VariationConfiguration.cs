using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.Infrastructure.Data.Configurations
{
    public class VariationConfiguration : IEntityTypeConfiguration<Variation>
    {
        public void Configure(EntityTypeBuilder<Variation> builder)
        {
			builder.HasQueryFilter(p => p.DeleteFlag != true);
			builder.HasOne(x => x.ProductItem).WithMany(x => x.Variations).HasForeignKey(x => x.ProductItemId);
            builder.HasOne(x => x.Size).WithMany(x => x.Variations).HasForeignKey(x => x.SizeId);
        }
    }
}
