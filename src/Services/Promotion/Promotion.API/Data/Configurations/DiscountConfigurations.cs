using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Promotion.API.Data.Configurations;

public class DiscountConfiguration : IEntityTypeConfiguration<Discount>
{
	public void Configure(EntityTypeBuilder<Discount> builder)
	{
		builder.HasQueryFilter(p => p.DeleteFlag != true);

		builder.HasOne(t => t.DiscountType).WithMany(t => t.Discounts).HasForeignKey(t => t.DiscountTypeId);
    }
}
