using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Catalog.Infrastructure.Data.Configurations
{
    public class BrandConfiguration : IEntityTypeConfiguration<Brand>
    {
        public void Configure(EntityTypeBuilder<Brand> builder)
        {
			builder.HasQueryFilter(p => p.DeleteFlag != true);
		}
    }
}
