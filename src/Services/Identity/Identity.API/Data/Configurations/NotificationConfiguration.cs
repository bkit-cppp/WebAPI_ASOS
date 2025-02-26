using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.API.Data.Configurations;

public class NotificationConfiguration : IEntityTypeConfiguration<Notification>
{
	public void Configure(EntityTypeBuilder<Notification> builder)
	{
		builder.HasQueryFilter(p => p.DeleteFlag != true);
		builder.HasOne(t => t.User).WithMany(t => t.Notifications).HasForeignKey(t => t.UserId);
	}
}

