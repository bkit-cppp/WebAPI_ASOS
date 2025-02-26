using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.Infrastructure.Data.Configurations;

public class CommentConfiguration : IEntityTypeConfiguration<Comment>
{
	public void Configure(EntityTypeBuilder<Comment> builder)
	{
		builder.HasQueryFilter(p => p.DeleteFlag != true);
		builder.HasOne(x => x.User).WithMany(x => x.Comments).HasForeignKey(x => x.UserId);
		builder.HasOne(x => x.Product).WithMany(x => x.Comments).HasForeignKey(x => x.ProductId);
	}
}
