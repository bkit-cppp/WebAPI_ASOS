using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.API.Data.Configurations;

public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
	public void Configure(EntityTypeBuilder<OrderItem> builder)
	{
		builder.HasOne(t => t.Order).WithMany(t => t.OrderItems).HasForeignKey(t => t.OrderId);
		builder.HasQueryFilter(p => p.DeleteFlag != true);
		builder.HasIndex(t => t.ProductId);
	}
}
