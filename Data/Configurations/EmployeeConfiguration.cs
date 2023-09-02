using Domain.Entities;
using Domain.Shared;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Data.Configurations
{
	public class EmployeeConfiguration : BaseConfiguration<Employee, Guid>
    {
        public override void Configure(EntityTypeBuilder<Employee> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.Title)
                .HasConversion(new EnumToStringConverter<EmployeeTitle>())
                .IsRequired();

            builder.Property(x => x.Email)
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(x => x.CreatedAt)
                .IsRequired();

            builder.HasIndex(x => x.Email).IsUnique();

            builder.HasMany(x => x.Companies).WithMany(x => x.Employees);
        }
	}
}

