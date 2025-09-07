using ManageProperties.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ManageProperties.Persist.Config
{
    public class OwnerConfig : IEntityTypeConfiguration<Owner>
    {
        public void Configure(EntityTypeBuilder<Owner> builder)
        {
            builder.ToTable("Owners");

            builder.Property(prop => prop.Name).HasMaxLength(50).IsRequired();
            builder.Property(prop => prop.Address).HasMaxLength(100);
            builder.Property(prop => prop.Photo).IsRequired();
            builder.Property(prop => prop.Birthday).HasMaxLength(10).IsRequired();
        }
    }
}
