using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Promotion.API.Data.Configurations;

public class DiscountProductConfiguration : IEntityTypeConfiguration<DiscountProduct>
{
	public void Configure(EntityTypeBuilder<DiscountProduct> builder)
	{
		builder.HasQueryFilter(p => p.DeleteFlag != true);
		builder.HasOne(t => t.Discount).WithMany(t => t.DiscountProducts).HasForeignKey(t => t.DiscountId);
	}
}
