using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Catalog.Infrastructure.Data.Configurations
{
    public class CategoryGenderConfiguration : IEntityTypeConfiguration<CategoryGender>
    {
        public void Configure(EntityTypeBuilder<CategoryGender> builder)
        {
			builder.HasQueryFilter(p => p.DeleteFlag != true);
			builder.HasOne(x => x.Gender).WithMany(x => x.CategoryGenders).HasForeignKey(x => x.GenderId);
			builder.HasOne(x => x.Category).WithMany(x => x.CategoryGenders).HasForeignKey(x => x.CategoryId);
		}
    }
}
