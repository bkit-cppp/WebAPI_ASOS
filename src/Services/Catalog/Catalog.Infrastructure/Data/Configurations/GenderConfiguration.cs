using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.Infrastructure.Data.Configurations;

public class GenderConfiguration : IEntityTypeConfiguration<Gender>
{
	public void Configure(EntityTypeBuilder<Gender> builder)
	{
		builder.HasQueryFilter(p => p.DeleteFlag != true);
		//builder.HasMany(x => x.Categories).WithMany(x => x.Genders);
	}
}
