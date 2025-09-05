using ManageProperties.Domain.Entities;
using ManagerProperties.Application.Contracts.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageProperties.Persist.Repositories
{
    public class RepositoryPropertyTraces:Repository<PropertyTrace>, IRepositoryPropertyTraces
    {
        public RepositoryPropertyTraces(ManagePropertiesDbContext context):base(context) 
        {

        }
    }
}
