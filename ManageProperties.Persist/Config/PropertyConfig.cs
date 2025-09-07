using ManageProperties.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ManageProperties.Persist.Config
{
    public class PropertyConfig : IEntityTypeConfiguration<Property>
    {
        public void Configure(EntityTypeBuilder<Property> builder)
        {
            builder.ToTable("Properties");

            builder.Property(prop => prop.CodeInternal).HasMaxLength(20).IsRequired();
            builder.Property(prop => prop.Name).HasMaxLength(60).IsRequired();
            builder.Property(prop => prop.Address).HasMaxLength(100);
            builder.Property(prop => prop.Price).HasPrecision(18,2).IsRequired();
            builder.Property(prop => prop.Year).HasMaxLength(4).IsRequired();

        }
    }
}
