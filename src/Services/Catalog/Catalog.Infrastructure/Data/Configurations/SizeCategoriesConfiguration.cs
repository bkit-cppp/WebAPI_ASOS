using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Catalog.Infrastructure.Data.Configurations
{
    public class SizeCategoriesConfiguration : IEntityTypeConfiguration<SizeCategory>
    {
        public void Configure(EntityTypeBuilder<SizeCategory> builder)
        {
			builder.HasQueryFilter(p => p.DeleteFlag != true);
			builder.HasOne(x => x.Size).WithMany(x => x.SizeCategories).HasForeignKey(x => x.SizeId);
            builder.HasOne(x => x.Category).WithMany(x => x.SizeCategories).HasForeignKey(x => x.CategoryId);
        }
    }
}
