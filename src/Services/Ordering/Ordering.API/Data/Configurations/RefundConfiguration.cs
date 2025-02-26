using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.API.Data.Configurations;

public class RefundConfiguration : IEntityTypeConfiguration<Refund>
{
	public void Configure(EntityTypeBuilder<Refund> builder)
	{
		builder.HasOne(t => t.Transaction).WithOne(t => t.Refund).HasForeignKey<Refund>(t => t.TransactionId);
		builder.HasQueryFilter(p => p.DeleteFlag != true);
	}
}
