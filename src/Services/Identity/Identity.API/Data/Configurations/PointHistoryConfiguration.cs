using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.API.Data.Configurations;

public class PointHistoryConfiguration : IEntityTypeConfiguration<PointHistory>
{
	public void Configure(EntityTypeBuilder<PointHistory> builder)
	{
		builder.HasQueryFilter(p => p.DeleteFlag != true);
		builder.HasOne(t => t.User).WithMany(t => t.PointHistories).HasForeignKey(t => t.UserId);
	}
}

