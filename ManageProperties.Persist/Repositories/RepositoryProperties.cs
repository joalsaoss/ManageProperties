using ManageProperties.Domain.Entities;
using ManagerProperties.Application.Contracts.Repositories;

namespace ManageProperties.Persist.Repositories
{
    public class RepositoryProperties:Repository<Property>, IRepositoryProperties 
    {
        public RepositoryProperties(ManagePropertiesDbContext context):base(context) 
        {

        }
    }
}
