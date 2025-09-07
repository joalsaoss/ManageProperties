using ManageProperties.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ManageProperties.Persist.Config
{
    public class PropertyTraceConfig : IEntityTypeConfiguration<PropertyTrace>
    {
        public void Configure(EntityTypeBuilder<PropertyTrace> builder)
        {
            builder.ToTable("PropertyTraces");

            builder.Property(prop => prop.DateSale).HasMaxLength(10).IsRequired();
            builder.Property(prop => prop.Name).HasMaxLength(50).IsRequired();
            builder.Property(prop => prop.Value).HasPrecision(18, 2).IsRequired();
            builder.Property(prop => prop.Tax).HasPrecision(4, 2).IsRequired();
        }
    }
}
