using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.API.Data.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
	public void Configure(EntityTypeBuilder<User> builder)
	{
		builder.HasOne(t => t.Role).WithMany(t => t.Users).HasForeignKey(t => t.RoleId);
		builder.HasOne(t => t.Status).WithMany(t => t.Users).HasForeignKey(t => t.StatusId);
		builder.HasQueryFilter(p => p.DeleteFlag != true);
	}
}
