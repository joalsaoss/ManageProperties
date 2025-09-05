using ManagerProperties.Application.Contracts.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ManageProperties.Persist.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ManagePropertiesDbContext context;

        public Repository(ManagePropertiesDbContext context)
        {
            this.context = context;
        }

        public Task<T> Create(T entity)
        {
            context.Add(entity);
            return Task.FromResult(entity);
        }

        public Task Update(T entity)
        {
            context.Update(entity);
            return Task.CompletedTask;
        }

        public Task Delete(T entity)
        {
            context.Remove(entity);
            return Task.CompletedTask;

        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await context.Set<T>().ToListAsync();
        }

        public async Task<T?> GetById(Guid id)
        {
            return await context.Set<T>().FindAsync(id);
        }

        
    }
}
