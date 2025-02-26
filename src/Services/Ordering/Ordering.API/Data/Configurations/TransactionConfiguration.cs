using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.API.Data.Configurations;

public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
{
	public void Configure(EntityTypeBuilder<Transaction> builder)
	{
		builder.HasOne(t => t.Order).WithOne(t => t.Transaction).HasForeignKey<Transaction>(t => t.OrderId);
		builder.HasOne(t => t.Refund).WithOne(t => t.Transaction).HasForeignKey<Transaction>(t => t.RefundId);
		builder.HasQueryFilter(p => p.DeleteFlag != true);
	}
}
