using ManageProperties.Domain.Entities;
using ManagerProperties.Application.Contracts.Identity;
using ManagerProperties.Domain.Commons;
using Microsoft.EntityFrameworkCore;

namespace ManageProperties.Persist
{
    public class ManagePropertiesDbContext : DbContext
    {
        private readonly IUserService? service;

        public ManagePropertiesDbContext(DbContextOptions<ManagePropertiesDbContext> options, 
            IUserService service) : base(options)
        {
            this.service = service;
        }

        protected ManagePropertiesDbContext()
        {
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            if (service is not null) 
            {
                foreach (var entry in ChangeTracker.Entries<AuditEntity>())
                {
                    switch (entry.State)
                    {
                        case EntityState.Added:
                            entry.Entity.CreatedOn = DateTime.UtcNow;
                            entry.Entity.CreatedBy = service.GetUserId();
                            break;
                        case EntityState.Modified:
                            entry.Entity.LastUpdatedOn = DateTime.UtcNow;
                            entry.Entity.LastUpdatedBy = service.GetUserId();
                            break;
                    }
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ManagePropertiesDbContext).Assembly);
        }
        public DbSet<Owner> Owners { get; set; }
        public DbSet<Property> Properties { get; set; }
        public DbSet<PropertyImage> PropertyImages { get; set; }
        public DbSet<PropertyTrace> PropertyTraces { get; set; }
        
    }
}
