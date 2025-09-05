using ManageProperties.Domain.Entities;
using ManagerProperties.Application.Contracts.Repositories;

namespace ManageProperties.Persist.Repositories
{
    public class RepositoryPropertyImages:Repository<PropertyImage>, IRepositoryPropertyImages 
    {
        public RepositoryPropertyImages(ManagePropertiesDbContext context) : base(context)
        {
            
        }
    }
}
