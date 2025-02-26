using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Promotion.API.Data.Configurations;

public class DiscountTypeConfiguration : IEntityTypeConfiguration<DiscountType>
{
	public void Configure(EntityTypeBuilder<DiscountType> builder)
	{
		builder.HasQueryFilter(p => p.DeleteFlag != true);
    }
}
