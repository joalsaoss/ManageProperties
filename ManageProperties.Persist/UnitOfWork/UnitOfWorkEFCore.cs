using ManagerProperties.Application.Contracts.Persists;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageProperties.Persist.UnitOfWork
{
    public class UnitOfWorkEFCore : IUnitOfWork
    {
        private readonly ManagePropertiesDbContext context;

        public UnitOfWorkEFCore(ManagePropertiesDbContext context)
        {
            this.context = context;
        }
        public async Task Persist()
        {
            await context.SaveChangesAsync();
        }

        public Task RollBack()
        {
            return Task.CompletedTask;
        }
    }
}
