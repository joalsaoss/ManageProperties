using ManageProperties.Domain.Entities;
using ManagerProperties.Application.Contracts.Repositories;

namespace ManageProperties.Persist.Repositories
{
    public class RepositoryOwners:Repository<Owner>, IRepositoryOwners
    {
        public RepositoryOwners(ManagePropertiesDbContext context):base(context) 
        {
            
        }

    }
}
