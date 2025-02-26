using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.Infrastructure.Data.Configurations
{
    public class RatingConfiguration : IEntityTypeConfiguration<Rating>
    {
        public void Configure(EntityTypeBuilder<Rating> builder)
        {
			builder.HasQueryFilter(p => p.DeleteFlag != true);
			builder.HasOne(x => x.Product).WithMany(x => x.Ratings).HasForeignKey(x => x.ProductId);
        }
    }
}
