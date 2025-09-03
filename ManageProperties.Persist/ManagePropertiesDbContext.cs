using ManageProperties.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ManageProperties.Persist
{
    public class ManagePropertiesDbContext : DbContext
    {
        public ManagePropertiesDbContext(DbContextOptions<ManagePropertiesDbContext> options) : base(options)
        {
        }

        protected ManagePropertiesDbContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ManagePropertiesDbContext).Assembly);
        }
        public DbSet<Owner> Owners { get; set; }


    }
}
