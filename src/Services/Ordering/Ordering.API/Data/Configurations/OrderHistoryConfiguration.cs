using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.API.Data.Configurations;

public class OrderHistoryConfiguration : IEntityTypeConfiguration<OrderHistory>
{
	public void Configure(EntityTypeBuilder<OrderHistory> builder)
	{
		builder.HasOne(t => t.Order).WithMany(t => t.OrderHistories).HasForeignKey(t => t.OrderId);
		builder.HasQueryFilter(p => p.DeleteFlag != true);
	}
}
