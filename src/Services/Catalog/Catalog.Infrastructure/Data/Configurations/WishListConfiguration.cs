using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.Infrastructure.Data.Configurations
{
    public class WishListConfiguration : IEntityTypeConfiguration<WishList>
    {
        public void Configure(EntityTypeBuilder<WishList> builder)
        {
			builder.HasQueryFilter(p => p.DeleteFlag != true);
			builder.HasOne(x => x.Product).WithMany(x => x.WishLists).HasForeignKey(x => x.ProductId);
        }
    }
}
