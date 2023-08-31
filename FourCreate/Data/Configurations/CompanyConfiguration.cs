using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations
{
	public class CompanyConfiguration : BaseConfiguration<Company, Guid>
	{
        public override void Configure(EntityTypeBuilder<Company> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.Name)
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(x => x.CreatedAt)
                .IsRequired();

            builder.HasIndex(x => x.Name).IsUnique();

            builder.HasMany(x => x.Employees).WithMany(x => x.Companies);
        }
    }
}

