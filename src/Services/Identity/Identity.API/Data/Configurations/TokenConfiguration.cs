using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.API.Data.Configurations;

public class TokenConfiguration : IEntityTypeConfiguration<Token>
{
	public void Configure(EntityTypeBuilder<Token> builder)
	{
		builder.HasOne(t => t.User).WithMany(t => t.Tokens).HasForeignKey(t => t.UserId);
		builder.HasQueryFilter(p => p.DeleteFlag != true);
	}
}
