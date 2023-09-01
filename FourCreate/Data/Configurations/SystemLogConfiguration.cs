using Domain.Entities;
using Domain.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Data.Configurations
{
	public class SystemLogConfiguration : BaseConfiguration<SystemLog, int>
    {
        public override void Configure(EntityTypeBuilder<SystemLog> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.ResourceType)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(x => x.ResourceId)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(x => x.Type)
                .HasConversion(new EnumToStringConverter<SystemLogType>())
                .IsRequired();

            builder.Property(x => x.Changes)
                .HasColumnType("json")
                .IsRequired()
                .IsUnicode();

            builder.Property(x => x.Comment)
                .IsRequired()
                .IsUnicode();
        }
	}
}

