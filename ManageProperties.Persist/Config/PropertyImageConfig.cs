using ManageProperties.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ManageProperties.Persist.Config
{
    public class PropertyImageConfig : IEntityTypeConfiguration<PropertyImage>
    {
        public void Configure(EntityTypeBuilder<PropertyImage> builder)
        {
            builder.ToTable("PropertyImages");

            builder.Property(x => x.PKey).HasMaxLength(900).IsRequired();
            builder.Property(x => x.ContentType).HasMaxLength(100).IsRequired();
            builder.Property(x => x.Bytes).IsRequired();
            builder.Property(x => x.CreatedAtUtc).IsRequired();
            builder.Property(prop => prop.Enable).HasMaxLength(1);
        }
    }
}
