using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.Infrastructure.Data.Configurations
{
    public class CategoriesConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
			builder.HasQueryFilter(p => p.DeleteFlag != true);
            //builder.HasMany(x => x.Genders).WithMany(x => x.Categories);
        }
    }
}
