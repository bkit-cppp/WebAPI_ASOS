using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.API.Data.Configurations;

public class OTPConfiguration : IEntityTypeConfiguration<OTP>
{
	public void Configure(EntityTypeBuilder<OTP> builder)
	{
		builder.HasQueryFilter(p => p.DeleteFlag != true);
		builder.HasOne(t => t.User).WithMany(t => t.OTPs).HasForeignKey(t => t.UserId);
	}
}

