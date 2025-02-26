using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.API.Data.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
	public void Configure(EntityTypeBuilder<Order> builder)
	{
		builder.HasOne(t => t.Transaction).WithOne(t => t.Order).HasForeignKey<Order>(t => t.TransactionId);
		builder.HasOne(t => t.Status).WithMany(t => t.Orders).HasForeignKey(t => t.StatusId);
		builder.HasQueryFilter(p => p.DeleteFlag != true);
		builder.HasIndex(t => t.UserId);
		builder.HasIndex(t => t.DiscountId);
	}
}
