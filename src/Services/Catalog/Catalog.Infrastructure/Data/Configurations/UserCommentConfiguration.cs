using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.Infrastructure.Data.Configurations;

public class UserCommentConfiguration : IEntityTypeConfiguration<UserComment>
{
	public void Configure(EntityTypeBuilder<UserComment> builder)
	{
		builder.HasQueryFilter(p => p.DeleteFlag != true);
	}
}
