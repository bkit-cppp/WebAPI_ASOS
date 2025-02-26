using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Promotion.API.Data.Configurations;

public class DiscountHistoryConfiguration : IEntityTypeConfiguration<DiscountHistory>
{
	public void Configure(EntityTypeBuilder<DiscountHistory> builder)
	{
		builder.HasQueryFilter(p => p.DeleteFlag != true);
		builder.HasOne(t => t.Discount).WithMany(t => t.DiscountHistories).HasForeignKey(t => t.DiscountId);
	}
}
