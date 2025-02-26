using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.API.Data.Configurations;

public class HubConnectionConfiguration : IEntityTypeConfiguration<HubConnection>
{
	public void Configure(EntityTypeBuilder<HubConnection> builder)
	{
		builder.HasOne(t => t.User).WithMany(t => t.HubConnections).HasForeignKey(t => t.UserId);
	}
}
